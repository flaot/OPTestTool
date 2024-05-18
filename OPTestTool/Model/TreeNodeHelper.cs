using OPTestTool.Common;
using OPTestTool.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTestTools.Model
{
    public class TreeNodeHelper
    {
        /// <summary>
        /// TreeNode选中节点颜色
        /// </summary>
        public Color treeNodeBackgroud { get; set; }

        /// <summary>
        /// 是否清除隐藏窗口
        /// </summary>
        public bool SetClearHide { get; set; } = false;

        private TreeView tv { get; set; } = new TreeView();
        private List<TreeNode> searchtreeNodes { get; set; } = new List<TreeNode>();
        private List<string> hwndEnumWindowParent { get; set; } = new List<string>();
        private List<string> hwndEnumWindowChild { get; set; } = new List<string>();
        private Dictionary<string, string> keyValuePairs { get; set; } = new Dictionary<string, string>();

        public OpSoft mk { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="treeView">TreeView对象</param>
        public TreeNodeHelper(TreeView treeView, OpSoft _mksoft) : this(treeView, _mksoft, "#001E26")
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="treeView">TreeView对象</param>
        /// <param name="color">颜色  #001E26</param>
        public TreeNodeHelper(TreeView treeView, OpSoft _mksoft, string color)
        {
            tv = treeView;
            treeNodeBackgroud = ColorTranslator.FromHtml(color);
            mk = _mksoft;
        }

        #region 增

        /// <summary>
        /// TreeViewAdd
        /// </summary>
        /// <param name="hwnd"></param>
        public void TreeViewAdd(int hwnd = 0)
        {
            hwndEnumWindowParent = mk.EnumWindow(0, "", "", 4).Split(',').ToList().Distinct().ToList();//#32769 1=2715 2=2715 4=805 8=424  16=513 32= 2715
            hwndEnumWindowChild = mk.EnumWindow(0, "", "", 1).Split(',').ToList().Distinct().ToList();
            keyValuePairs = mk.FindParent(hwndEnumWindowChild, hwndEnumWindowParent);

            TreeNode tn = new TreeNode(Win32.GetDesktopWindows().ToString());

            tv.BeginUpdate();
            tv.Nodes.Clear();

            var processIco = Win32.GetProcessIco(mk);
            tv.ImageList = processIco.Item1;

            if (hwnd == 0)
            {
                tv.Nodes.Add(Add(tn, keyValuePairs, hwndEnumWindowParent, processIco.Item2));
            }
            else
            {
                List<string> TempValue = new List<string>();
                TempValue.Add(hwnd.ToString());
                tv.Nodes.Add(Add(tn, keyValuePairs, TempValue, processIco.Item2));
                tv.ExpandAll();
            }
            tv.EndUpdate();
            tv.ExpandAll();
            if (hwnd != 0)
                FindTreeView(hwnd.ToString());
        }

        /// <summary>
        /// TreeView窗体添加数据以及图片
        /// </summary>
        /// <param name="r">默认树</param>
        /// <param name="keyValues">Key=子窗口，Values=父窗口</param>
        /// <param name="parent">父窗口</param>
        /// <param name="icoIndex">图片与进程对应的索引</param>
        /// <returns>TreeNode</returns>
        private TreeNode Add(TreeNode r, Dictionary<string, string> keyValues, List<string> parent, Dictionary<int, int> icoIndex)
        {
            List<string> ct = new List<string>();
            TreeNode tree = null;
            foreach (var p in parent)
            {
                bool windowState = mk.IsWindowsVisible(p.ToInt32());           //获取窗口是否显示

                if (SetClearHide)                                          //是否清理隐藏窗口
                {
                    if (windowState == false)                              //不可见则去除，不进行添加
                        continue;
                }
                tree = new TreeNode(mk.GetWindowHwndTitleClassName(p));
                r.Nodes.Add(tree);

                if (windowState)                               //默认把可见查看图片索引置换为1
                {
                    tree.ImageIndex = 1;
                }

                if (icoIndex.ContainsKey(p.ToInt32()))
                {
                    int cindex = icoIndex[p.ToInt32()];
                    tree.ImageIndex = cindex;
                    tree.SelectedImageIndex = cindex;
                }
                foreach (var item in keyValues)
                {
                    if (item.Value == p)
                    {
                        ct.Clear();
                        ct.Add(item.Key);//key是子窗口，递归就是需要查找子窗口下面是否还有子窗口
                        Add(tree, keyValues, ct, icoIndex);
                    }
                }
            }
            return r;
        }

        #endregion 增

        #region 查

        /// <summary>
        /// 搜索TreeView数据
        /// </summary>
        /// <param name="content"></param>
        public void FindTreeView(string content)
        {
            searchtreeNodes = new List<TreeNode>();
            foreach (TreeNode node in tv.Nodes)    //tvMapLyMgr是TreeView控件的名称
            {
                SearchLayer(node, content);
            }
            for (int i = 0; i < searchtreeNodes.Count; i++)
            {
                TreeNode temp = searchtreeNodes[i];
                ExpandNode(temp);
                if (i == 0 && temp != null)
                {
                    tv.SelectedNode = temp;
                }
                temp.BackColor = treeNodeBackgroud;
                temp.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 匹配节点
        /// </summary>
        /// <param name="node">TreeNode</param>
        /// <param name="name">匹配数据</param>
        private void SearchLayer(TreeNode node, string name)
        {
            if (node.Nodes.Count != 0)
            {
                if (string.Equals(node.Text, name) | node.Text.Contains(name))
                {
                    searchtreeNodes.Add(node);
                }
                else
                {
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        SearchLayer(node.Nodes[i], name);
                    }
                }
            }
            else if (string.Equals(node.Text, name) | node.Text.Contains(name))
            {
                searchtreeNodes.Add(node);
            }
        }

        /// <summary>
        /// 展开父节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Expand();
                ExpandNode(node.Parent);
            }
        }

        #endregion 查
    }
}