using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 监考分配
{
    public partial class Form1 : Form
    {
        private NumOfday[] numofday { get; set; }
        private int day_num { get; set; }
        private DataTable dttable; // 监考表
        private DataTable Teaching_Office; // 教研室表
        private DataTable flag; // 教研室名称，日期，时间
        private List<TearchingOffice> iner_TearchingOffice; // 教研室的信息

        public Form1()
        {
            InitializeComponent();
        }

        private static DataTable ReadExcel(string fileName, int headerRowNumber)
        {
            DataTable dtTable = new DataTable();
            ISheet sheet;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                stream.Position = 0;
                IWorkbook workbook = null;
                if (fileName.IndexOf(".xlsx") > 0)
                    workbook = new XSSFWorkbook(stream);
                else if (fileName.IndexOf(".xls") > 0)
                    workbook = new HSSFWorkbook(stream);

                if (workbook == null) return null;

                sheet = workbook.GetSheetAt(0);
                if (sheet == null) return null;

                // 列名
                IRow headerRow = sheet.GetRow(headerRowNumber);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    dtTable.Columns.Add(cell.ToString());
                }
                // 填充行
                for (int i = headerRowNumber + 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    DataRow newRow = dtTable.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Boolean:
                                    newRow[j] = cell.BooleanCellValue.ToString();
                                    break;
                                case CellType.String:
                                    newRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Numeric:
                                    if (cell.CellStyle.DataFormat != 0 && DateUtil.IsCellDateFormatted(cell))
                                        newRow[j] = cell.DateCellValue.ToString("yyyy年MM月dd日");
                                    else
                                        newRow[j] = cell.NumericCellValue.ToString();
                                    break;
                                default:
                                    newRow[j] = "";
                                    break;
                            }
                        }
                        else
                        {
                            newRow[j] = "";
                        }
                    }
                    dtTable.Rows.Add(newRow);
                }
            }
            return dtTable;
        }

        private void WriteExcel(DataTable table, string outputFileName)
        {
            var memoryStream = new MemoryStream();

            using (var fs = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Sheet1");

                List<String> columns = new List<string>();
                IRow row = excelSheet.CreateRow(0);
                int columnIndex = 0;

                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                    columnIndex++;
                }

                int rowIndex = 1;
                foreach (DataRow dsrow in table.Rows)
                {
                    row = excelSheet.CreateRow(rowIndex);
                    int cellIndex = 0;
                    foreach (String col in columns)
                    {
                        row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                        cellIndex++;
                    }
                    rowIndex++;
                }
                workbook.Write(fs);
            }
        }

        //考试周一共多少天,每天的每个时候都有多少考试
        private int GetDayNum()
        {
            List<string> daylist = new List<string>();
            foreach (DataRow row in this.dttable.Rows)
            {
                if (!daylist.Exists(t => t == row["日期"].ToString()))
                    daylist.Add(row["日期"].ToString());
            }
            numofday = new NumOfday[daylist.Count];
            for (int i = 0; i < daylist.Count; i++)
            {
                numofday[i] = new NumOfday();
                numofday[i].dtime = daylist[i];
                numofday[i].num = new int[3];
                for (int j = 0; j < 3; j++)
                    numofday[i].num[j] = 0;
            }
            foreach (DataRow row in this.dttable.Rows)
            {
                for (int i = 0; i < daylist.Count; i++)
                {
                    if (row["日期"].ToString() == numofday[i].dtime)
                    {
                        string time = row["时间"].ToString();
                        if (check_when(time) == "上午")
                            numofday[i].num[0] += StrToInt(row["监考安排"].ToString());
                        else if (check_when(time) == "下午")
                            numofday[i].num[1] += StrToInt(row["监考安排"].ToString());
                        else numofday[i].num[2] += StrToInt(row["监考安排"].ToString());
                    }
                }
            }
            return daylist.Count;
        }

        private int StrToInt(string str)
        {
            int stu_num = 0;
            try
            {
                stu_num = Convert.ToInt32(str);
                return stu_num;
            }
            catch (FormatException ee)
            {
                return 0;
            }
        }

        //判断是上午下午还是晚上
        private string check_when(string time)
        {
            string[] timearry = time.Split(new char[1] { ':' });
            int hour = 0;
            try
            {
                hour = Convert.ToInt32(timearry[0]);
            }
            catch (FormatException ee)
            {
                Console.WriteLine("出错了");
            }
            string time_when;
            if (hour < 12)
                time_when = "上午";
            else if (hour >= 12 && hour < 18)
                time_when = "下午";
            else
                time_when = "晚上";
            return time_when;
        }

        // 读取限制信息
        private DataTable getLimit()
        {
            DataTable limitTable = new DataTable();

            DataColumn dc0 = new DataColumn("教研室", typeof(string));
            DataColumn dc1 = new DataColumn("日期", typeof(string));
            DataColumn dc2 = new DataColumn("时间", typeof(string));
            limitTable.Columns.Add(dc0);
            limitTable.Columns.Add(dc1);
            limitTable.Columns.Add(dc2);

            foreach (DataGridViewRow row in limitGridView.Rows)
            {
                DataRow newRow = limitTable.NewRow();
                newRow["教研室"] = row.Cells["name"];
                newRow["日期"] = row.Cells["date"];
                newRow["时间"] = row.Cells["time"];
                limitTable.Rows.Add(newRow);
            }

            return limitTable;
        }

        //判断这个时间段这个教研室是否受到了限制
        private int check_limit(string dtime, int time, string id)
        {
            string time_when = "";
            if (time == 0)
                time_when = "上午";
            else if (time == 1)
                time_when = "下午";
            else
                time_when = "晚上";
            foreach (DataRow row in flag.Rows)
            {
                if (row["日期"].ToString() == dtime && row["时间"].ToString() == time_when && row["教研室"].ToString() == id)
                    return 1;
            }



            return 0;
        }

        // 把教研室表的信息实例化
        private List<TearchingOffice> GetTearchingOffice()
        {
            List<TearchingOffice> tolist = new List<TearchingOffice>();
            int sum = 0;
            foreach (DataRow row in this.Teaching_Office.Rows)
            {
                TearchingOffice torow = new TearchingOffice();
                torow.id = row["教研室"].ToString();
                torow.tea_name = row["主任"].ToString();

                try
                {
                    torow.tea_num = Convert.ToInt32(row["人数"].ToString());
                }
                catch (FormatException ee)
                {
                    Console.WriteLine("出错了");
                }
                sum += torow.tea_num;
                tolist.Add(torow);
            }

            for (int i = 0; i < tolist.Count; i++)
            {
                var portation = 1.0 * tolist[i].tea_num / sum;
                tolist[i].portation = portation;
            }

            return tolist;
        }

        //为每个教研室计算预期应该给他们分配的最大数量
        private void GetDistriNum()
        {
            for (int i = 0; i < iner_TearchingOffice.Count; i++)
            {
                iner_TearchingOffice[i].day_tea_num = new int[day_num];
                for (int j = 0; j < day_num; j++)
                {
                    iner_TearchingOffice[i].day_tea_num[j] = new int();
                    iner_TearchingOffice[i].day_tea_num[j] = (int)((numofday[j].num[0] + +numofday[j].num[1] + numofday[j].num[2]) * iner_TearchingOffice[i].portation) + 1;//加1
                }
            }
        }

        //按照每一上午来分配
        private void DistuDay()
        {
            // 先给所有的字典初始化
            for (int i = 0; i < day_num; i++)
            {
                numofday[i].distru = new Dictionary<int, int>[3];
                for (int j = 0; j < 3; j++)
                {
                    numofday[i].distru[j] = new Dictionary<int, int>();
                }
            }
            for (int i = 0; i < day_num; i++) // 每一天
            {
                Random rd = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
                for (int j = 0; j < 3; j++) // 每一上午
                {
                    for (int k = 0; k < iner_TearchingOffice.Count; k++)
                        iner_TearchingOffice[k].already_num = 0; // 每次对某一天某一时间，已经分配的数量清零
                    int distur_num = 0;
                    while (distur_num < numofday[i].num[j])
                    {
                        // 先判断是不是所有的教研室的数量都到预计了但是还是没分配够
                        int flag = 1;
                        for (int k = 0; k < iner_TearchingOffice.Count; k++)
                        {
                            if (iner_TearchingOffice[k].already_num < iner_TearchingOffice[k].day_tea_num[j])
                            {
                                flag = 0;
                                break; // 这个时候就是说明还有能继续分配的教研室
                            }
                        }
                        int tea_id = rd.Next(iner_TearchingOffice.Count);
                        if (check_limit(numofday[i].dtime, j, iner_TearchingOffice[tea_id].id) == 1)
                            Console.WriteLine("{0}{1}{2}被限制住了", tea_id, numofday[i].dtime, j);
                        if (flag == 0)
                        {
                            // 一是分配的人数已经达到了科室人数，二是已经达到了应该分配的上限,三是受到了限制
                            while (iner_TearchingOffice[tea_id].day_tea_num[j] < iner_TearchingOffice[tea_id].already_num ||
                                iner_TearchingOffice[tea_id].already_num >= iner_TearchingOffice[tea_id].tea_num ||
                                check_limit(numofday[i].dtime, j, iner_TearchingOffice[tea_id].id) == 1) // 这时候找到的随机教研室是不符合要求的
                            {
                                tea_id = rd.Next(iner_TearchingOffice.Count); // 继续找到合适的教研室
                            }
                            // 对合适的教研室进行操作,每次分配一个人
                            if (numofday[i].distru[j].ContainsKey(tea_id))
                                numofday[i].distru[j][tea_id]++;
                            else // 该科室还没有分配人员
                            {
                                numofday[i].distru[j].Add(tea_id, 1);//分配一个
                            }
                        }
                        else
                        {
                            // 这时候就是因为限制条件导致原有的策略失效了
                            // 这个时候就要看是否有还没到人数的教研室,此时肯定所有的教研室分配的数量都达到了预期
                            while (iner_TearchingOffice[tea_id].already_num >= iner_TearchingOffice[tea_id].tea_num ||
                                check_limit(numofday[i].dtime, j, iner_TearchingOffice[tea_id].id) == 1) // 这时候找到的随机教研室是不符合要求的
                            {
                                tea_id = rd.Next(iner_TearchingOffice.Count); // 继续找到合适的教研室
                            }
                            // 字典中该键值对一定是存在的
                            numofday[i].distru[j][tea_id]++;
                        }
                        // 修改各种值
                        distur_num++;
                        iner_TearchingOffice[tea_id].already_num++;
                    }
                }
            }
        }

        //最终的分配方案
        private void DistuFinal()
        {
            foreach (DataRow row in this.dttable.Rows)
            {
                string dtime = row["日期"].ToString();
                int dtime_in;
                if (check_when(row["时间"].ToString()) == "上午")
                    dtime_in = 0;
                else if (check_when(row["时间"].ToString()) == "下午")
                    dtime_in = 1;
                else
                    dtime_in = 2;
                int num = StrToInt(row["监考安排"].ToString());
                int remain_num = num;
                string fin_str = "";
                for (int i = 0; i < day_num; i++)
                {
                    if (numofday[i].dtime == dtime) // 找到相对应的时间点
                    {
                        foreach (int key in numofday[i].distru[dtime_in].Keys.ToArray())
                        {
                            if (remain_num > 0)
                            {
                                if (numofday[i].distru[dtime_in][key] <= remain_num)
                                {   // 在字典中删除这个元素，修改final字符串，修改剩余的数量
                                    string tea_name = iner_TearchingOffice[key].tea_name;
                                    int tea_num = numofday[i].distru[dtime_in][key];
                                    numofday[i].distru[dtime_in].Remove(key); // 删除
                                    remain_num -= tea_num;
                                    fin_str = fin_str + "+" + tea_name + tea_num.ToString() + "人";
                                }
                                else
                                {   // 修改字典中的值
                                    string tea_name = iner_TearchingOffice[key].tea_name;
                                    int tea_num = remain_num;
                                    numofday[i].distru[dtime_in][key] -= remain_num;
                                    remain_num = 0;
                                    fin_str = fin_str + "+" + tea_name + tea_num.ToString() + "人";
                                }
                            }
                            else
                                break;
                        }
                    }
                }
                row["监考安排"] = fin_str.Substring(1);
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            // 打开文件对话框
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开监考文件";
            dialog.InitialDirectory = "c://";
            dialog.Filter = "xls表格|*.xls|xlsx表格|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dttable = ReadExcel(dialog.FileName, 1);
                this.dataGridView1.DataSource = dttable;
            }
            // 将日期添加到下拉列表中
            var array = from DataRow row in dttable.Rows select row["日期"];
            this.date.Items.AddRange(array.ToHashSet().ToArray());
        }

        private void openTeachingOfficeFile_Click(object sender, EventArgs e)
        {
            // 打开文件对话框
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开教研室文件";
            dialog.InitialDirectory = "c://";
            dialog.Filter = "xls表格|*.xls|xlsx表格|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Teaching_Office = ReadExcel(dialog.FileName, 0);
                this.dataGridView2.DataSource = Teaching_Office;
            }
            // 将教研室的名字添加到下拉列表中
            var array = from DataRow row in Teaching_Office.Rows select row["教研室"];
            this.name.Items.AddRange(array.ToHashSet().ToArray());
        }

        private void cal_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(this, "请先读入考试信息", "提示");
                return;
            }
            if (this.dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show(this, "请先读入教研室信息", "提示");
                return;
            }
            // 计算监考人数
            foreach (DataRow row in this.dttable.Rows)
            {
                //Console.WriteLine(check_when( row["时间"].ToString()));
                if (row["人数"].ToString() == "")
                {
                    int num = 2;
                    row["监考安排"] = num.ToString();
                }
                else
                {
                    int stu_num = 0;
                    try
                    {
                        stu_num = Convert.ToInt32(row["人数"].ToString());
                    }
                    catch (FormatException ee)
                    {
                        Console.WriteLine("出错了");
                    }
                    int te_num = 0;
                    if (stu_num < 70)
                        te_num = 2;
                    else if (stu_num >= 70 && stu_num <= 100)
                        te_num = 3;
                    else if (stu_num > 100 && stu_num <= 150)
                        te_num = 4;
                    else if (stu_num > 150 && stu_num <= 180)
                        te_num = 5;
                    else
                        te_num = 6;
                    row["监考安排"] = te_num.ToString();
                }
            }
            // 产生监考计划
            flag = getLimit();
            iner_TearchingOffice = GetTearchingOffice();
            day_num = GetDayNum(); // 获得限制条件
            // Console.WriteLine(day_num);
            GetDistriNum();
            DistuDay();
            DistuFinal();
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "另存为";
            dialog.InitialDirectory = "c://";
            dialog.Filter = "xls表格|*.xls|xlsx表格|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                WriteExcel(dttable, dialog.FileName);
            }
        }

        private void addLimitButton_Click(object sender, EventArgs e)
        {
            this.limitGridView.Rows.Add();
        }

        private void limitGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn column = this.limitGridView.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    this.limitGridView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
    }

    class TearchingOffice // 教研室类别
    {
        public string id { get; set; } // 教研室的名称
        public int tea_num { get; set; } // 人数
        public string tea_name { get; set; } // 教研室主任的名字
        public double portation { get; set; } // 人数占的比例
        public int[] day_tea_num { get; set; }
        public int already_num { get; set; }
    }

    class NumOfday
    {
        public string dtime { get; set; }
        public int[] num { get; set; } // 0是上午，1是下午，2是晚上,数值分别代表需要多少人，加起来就是这一天的考试需要的监考人数
        public Dictionary<int, int>[] distru { get; set; } // 分配，同样0,1,2三个上午
    }
}
