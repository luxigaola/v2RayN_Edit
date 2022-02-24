using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace v2rayN.Sorter
{
    public class ListViewColumnSorter:IComparer
    {
        private readonly CaseInsensitiveComparer _objectCompare;
        public ListViewColumnSorter()
        {
            ColumnToSort = 0;

            OrderOfSort = SortOrder.None;

            _objectCompare = new CaseInsensitiveComparer();
        }
        public ListViewColumnSorter(int columnToSort,SortOrder orderOfSort)
        {
            ColumnToSort = columnToSort;

            OrderOfSort = SortOrder.None;

            _objectCompare = new CaseInsensitiveComparer();
        }
        /// <summary>
        /// 重写IComparer接口.
        /// </summary>
        /// <param name="x">要比较的第一个对象</param>
        /// <param name="y">要比较的第二个对象</param>
        /// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
        public int Compare(object x, object y)
        {
            var compareResult = 0;
            //  将比较对象转换为ListViewItem对象
            var listViewX = (ListViewItem)x;
            var listViewY = (ListViewItem)y;
            var textResultX = listViewX?.SubItems[ColumnToSort].Text;
            var textResultY = listViewY?.SubItems[ColumnToSort].Text;
            if (textResultX.Contains("M/s") && textResultY.Contains("M/s"))
            {
                var xVal = float.Parse(textResultX.Replace("M/s", ""));
                var yVal = float.Parse(textResultY.Replace("M/s", ""));
                //比较
                compareResult = _objectCompare.Compare(xVal,
                    yVal);
            }
            else
            {
                //比较
                compareResult = _objectCompare.Compare(listViewX?.SubItems[ColumnToSort].Text,
                    listViewY?.SubItems[ColumnToSort].Text);
            }
            

            switch (OrderOfSort)
            {
                case SortOrder.Ascending:
                    return compareResult;
                case SortOrder.Descending:
                    return (-compareResult);
                default:
                    return 0;
            }
        }

        public int ColumnToSort { get; set; }

        public SortOrder OrderOfSort { get; set; }
    }
}
