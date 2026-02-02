/**
This is an automatically generated class by OpExport. Please do not modify it.
License：https://github.com/WallBreaker2/op/blob/master/LICENSE 
**/

using System;
using System.Runtime.InteropServices;
public partial class OpSoft: IDisposable, IComparable<OpSoft>
{
#if X86
    const string DLL_NAME = "./Dll/op_x32.dll";
#else
    const string DLL_NAME = "./Dll/op_x64.dll";
#endif

    private IntPtr _op; // 非托管资源
    private IntPtr _pStr = IntPtr.Zero;
    private int _nSize = 0;

    #region Dispose
    private bool disposed = false;   // 是否已经释放资源的标志
    public OpSoft()
    {
        _op = OP_CreateOP();
    }
    ~OpSoft() => Dispose(false);
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
            }
            OP_ReleaseOP(_op);
            _op = IntPtr.Zero;
            if (_nSize > 0)
            {
                Marshal.FreeHGlobal(_pStr);
                _pStr = IntPtr.Zero;
                _nSize = 0;
            }
        }
        disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
    #region Overroid
    public override bool Equals(object obj) => obj is OpSoft soft && GetID() == soft.GetID();
    public override int GetHashCode() => GetID().GetHashCode();
    public override string ToString() => string.Format("id:{0}", GetID());
    public int CompareTo(OpSoft other) => GetID().CompareTo(other.GetID());
    #endregion
    /// <summary>
    /// 给指定的字库中添加一条字库信息
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="dict_info"></param>
    public int AddDict(int idx, string dict_info) => OP_AddDict(_op, idx, dict_info);
    /// <summary>
    /// A星算法
    /// </summary>
    /// <param name="mapWidth"></param>
    /// <param name="mapHeight"></param>
    /// <param name="disable_points"></param>
    /// <param name="beginX"></param>
    /// <param name="beginY"></param>
    /// <param name="endX"></param>
    /// <param name="endY"></param>
    public string AStarFindPath(int mapWidth, int mapHeight, string disable_points, int beginX, int beginY, int endX, int endY)
    {
        int _size = OP_AStarFindPath(_op, mapWidth, mapHeight, disable_points, beginX, beginY, endX, endY, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_AStarFindPath(_op, mapWidth, mapHeight, disable_points, beginX, beginY, endX, endY, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// bind window and beign capture screen
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="display"></param>
    /// <param name="mouse"></param>
    /// <param name="keypad"></param>
    /// <param name="mode"></param>
    public int BindWindow(int hwnd, string display, string mouse, string keypad, int mode) => OP_BindWindow(_op, hwnd, display, mouse, keypad, mode);
    /// <summary>
    /// 抓取指定区域(x1, y1, x2, y2)的图像, 保存为file
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="file_name"></param>
    public int Capture(int x1, int y1, int x2, int y2, string file_name) => OP_Capture(_op, x1, y1, x2, y2, file_name);
    /// <summary>
    /// 取上次操作的图色区域，保存为file(24位位图)
    /// </summary>
    /// <param name="file_name"></param>
    public int CapturePre(string file_name) => OP_CapturePre(_op, file_name);
    /// <summary>
    /// 清空指定的字库
    /// </summary>
    /// <param name="idx"></param>
    public int ClearDict(int idx) => OP_ClearDict(_op, idx);
    /// <summary>
    /// 把窗口坐标转换为屏幕坐标
    /// </summary>
    /// <param name="ClientToScreen"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int ClientToScreen(int ClientToScreen, ref int x, ref int y) => OP_ClientToScreen(_op, ClientToScreen, ref x, ref y);
    /// <summary>
    /// 比较指定坐标点(x,y)的颜色
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    public int CmpColor(int x, int y, string color, double sim) => OP_CmpColor(_op, x, y, color, sim);
    /// <summary>
    /// 延时指定的毫秒,过程中不阻塞UI操作
    /// </summary>
    /// <param name="mis"></param>
    public int Delay(int mis) => OP_Delay(_op, mis);
    /// <summary>
    /// 延时指定范围内随机毫秒,过程中不阻塞UI操作
    /// </summary>
    /// <param name="mis_min"></param>
    /// <param name="mis_max"></param>
    public int Delays(int mis_min, int mis_max) => OP_Delays(_op, mis_min, mis_max);
    /// <summary>
    /// 设置是否开启或者关闭插件内部的图片缓存机制
    /// </summary>
    /// <param name="enable"></param>
    public int EnablePicCache(int enable) => OP_EnablePicCache(_op, enable);
    /// <summary>
    /// 根据指定进程名,枚举系统中符合条件的进程PID
    /// </summary>
    /// <param name="name"></param>
    public string EnumProcess(string name)
    {
        int _size = OP_EnumProcess(_op, name, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_EnumProcess(_op, name, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 根据指定条件,枚举系统中符合条件的窗口
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="title"></param>
    /// <param name="class_name"></param>
    /// <param name="filter"></param>
    public string EnumWindow(int parent, string title, string class_name, int filter)
    {
        int _size = OP_EnumWindow(_op, parent, title, class_name, filter, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_EnumWindow(_op, parent, title, class_name, filter, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 根据指定进程以及其它条件,枚举系统中符合条件的窗口
    /// </summary>
    /// <param name="process_name"></param>
    /// <param name="title"></param>
    /// <param name="class_name"></param>
    /// <param name="filter"></param>
    public string EnumWindowByProcess(string process_name, string title, string class_name, int filter)
    {
        int _size = OP_EnumWindowByProcess(_op, process_name, title, class_name, filter, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_EnumWindowByProcess(_op, process_name, title, class_name, filter, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 根据指定的范围,以及指定的颜色描述，提取点阵信息，类似于大漠工具里的单独提取
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="word"></param>
    public string FetchWord(int x1, int y1, int x2, int y2, string color, string word)
    {
        int _size = OP_FetchWord(_op, x1, y1, x2, y2, color, word, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FetchWord(_op, x1, y1, x2, y2, color, word, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 查找指定区域内的颜色
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int FindColor(int x1, int y1, int x2, int y2, string color, double sim, int dir, out int x, out int y) => OP_FindColor(_op, x1, y1, x2, y2, color, sim, dir, out x, out y);
    /// <summary>
    /// 查找指定区域内的颜色块,颜色格式"RRGGBB-DRDGDB",注意,和按键的颜色格式相反
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    /// <param name="count"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int FindColorBlock(int x1, int y1, int x2, int y2, string color, double sim, int count, int height, int width, out int x, out int y) => OP_FindColorBlock(_op, x1, y1, x2, y2, color, sim, count, height, width, out x, out y);
    /// <summary>
    /// 查找指定区域内的所有颜色块, 颜色格式"RRGGBB-DRDGDB", 注意, 和按键的颜色格式相反
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    /// <param name="count"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    public string FindColorBlockEx(int x1, int y1, int x2, int y2, string color, double sim, int count, int height, int width)
    {
        int _size = OP_FindColorBlockEx(_op, x1, y1, x2, y2, color, sim, count, height, width, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindColorBlockEx(_op, x1, y1, x2, y2, color, sim, count, height, width, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 查找指定区域内的所有颜色
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    public string FindColorEx(int x1, int y1, int x2, int y2, string color, double sim, int dir)
    {
        int _size = OP_FindColorEx(_op, x1, y1, x2, y2, color, sim, dir, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindColorEx(_op, x1, y1, x2, y2, color, sim, dir, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 查找频幕中的直线
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    public string FindLine(int x1, int y1, int x2, int y2, string color, double sim)
    {
        int _size = OP_FindLine(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindLine(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 根据指定的多点查找颜色坐标
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="first_color"></param>
    /// <param name="offset_color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int FindMultiColor(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out int x, out int y) => OP_FindMultiColor(_op, x1, y1, x2, y2, first_color, offset_color, sim, dir, out x, out y);
    /// <summary>
    /// 根据指定的多点查找所有颜色坐标
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="first_color"></param>
    /// <param name="offset_color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    public string FindMultiColorEx(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir)
    {
        int _size = OP_FindMultiColorEx(_op, x1, y1, x2, y2, first_color, offset_color, sim, dir, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindMultiColorEx(_op, x1, y1, x2, y2, first_color, offset_color, sim, dir, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="all_pos"></param>
    /// <param name="type"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public string FindNearestPos(string all_pos, int type, int x, int y)
    {
        int _size = OP_FindNearestPos(_op, all_pos, type, x, y, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindNearestPos(_op, all_pos, type, x, y, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 查找指定区域内的图片
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="files"></param>
    /// <param name="delta_color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int FindPic(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, out int x, out int y) => OP_FindPic(_op, x1, y1, x2, y2, files, delta_color, sim, dir, out x, out y);
    /// <summary>
    /// 查找多个图片
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="files"></param>
    /// <param name="delta_color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    public string FindPicEx(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir)
    {
        int _size = OP_FindPicEx(_op, x1, y1, x2, y2, files, delta_color, sim, dir, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindPicEx(_op, x1, y1, x2, y2, files, delta_color, sim, dir, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 这个函数可以查找多个图片, 并且返回所有找到的图像的坐标.此函数同FindPicEx.只是返回值不同.(file1,x,y|file2,x,y|...)
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="files"></param>
    /// <param name="delta_color"></param>
    /// <param name="sim"></param>
    /// <param name="dir"></param>
    public string FindPicExS(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir)
    {
        int _size = OP_FindPicExS(_op, x1, y1, x2, y2, files, delta_color, sim, dir, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindPicExS(_op, x1, y1, x2, y2, files, delta_color, sim, dir, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 在屏幕范围(x1,y1,x2,y2)内,查找string(可以是任意个字符串的组合),并返回符合color_format的坐标位置
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="strs"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    /// <param name="retx"></param>
    /// <param name="rety"></param>
    public int FindStr(int x1, int y1, int x2, int y2, string strs, string color, double sim, out int retx, out int rety) => OP_FindStr(_op, x1, y1, x2, y2, strs, color, sim, out retx, out rety);
    /// <summary>
    /// 返回符合color_format的所有坐标位置
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="strs"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    public string FindStrEx(int x1, int y1, int x2, int y2, string strs, string color, double sim)
    {
        int _size = OP_FindStrEx(_op, x1, y1, x2, y2, strs, color, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_FindStrEx(_op, x1, y1, x2, y2, strs, color, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 查找符合类名或者标题名的顶层可见窗口
    /// </summary>
    /// <param name="class_name"></param>
    /// <param name="title"></param>
    public int FindWindow(string class_name, string title) => OP_FindWindow(_op, class_name, title);
    /// <summary>
    /// 根据指定的进程名字，来查找可见窗口
    /// </summary>
    /// <param name="process_name"></param>
    /// <param name="class_name"></param>
    /// <param name="title"></param>
    public int FindWindowByProcess(string process_name, string class_name, string title) => OP_FindWindowByProcess(_op, process_name, class_name, title);
    /// <summary>
    /// 根据指定的进程Id，来查找可见窗口
    /// </summary>
    /// <param name="process_id"></param>
    /// <param name="class_name"></param>
    /// <param name="title"></param>
    public int FindWindowByProcessId(int process_id, string class_name, string title) => OP_FindWindowByProcessId(_op, process_id, class_name, title);
    /// <summary>
    /// 查找符合类名或者标题名的顶层可见窗口,如果指定了parent,则在parent的第一层子窗口中查找
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="class_name"></param>
    /// <param name="title"></param>
    public int FindWindowEx(int parent, string class_name, string title) => OP_FindWindowEx(_op, parent, class_name, title);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file_name"></param>
    public int FreePic(string file_name) => OP_FreePic(_op, file_name);
    /// <summary>
    /// 获取插件目录
    /// </summary>
    public string GetBasePath()
    {
        int _size = OP_GetBasePath(_op, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetBasePath(_op, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取当前对象已经绑定的窗口句柄. 无绑定返回0
    /// </summary>
    public int GetBindWindow() => OP_GetBindWindow(_op);
    /// <summary>
    /// 获取窗口客户区域在屏幕上的位置
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    public int GetClientRect(int hwnd, out int x1, out int y1, out int x2, out int y2) => OP_GetClientRect(_op, hwnd, out x1, out y1, out x2, out y2);
    /// <summary>
    /// 获取窗口客户区域的宽度和高度
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public int GetClientSize(int hwnd, out int width, out int height) => OP_GetClientSize(_op, hwnd, out width, out height);
    /// <summary>
    /// 获取剪贴板数据
    /// </summary>
    public string GetClipboard()
    {
        int _size = OP_GetClipboard(_op, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetClipboard(_op, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 运行命令行并返回结果
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="millseconds"></param>
    public string GetCmdStr(string cmd, int millseconds)
    {
        int _size = OP_GetCmdStr(_op, cmd, millseconds, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetCmdStr(_op, cmd, millseconds, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取(x,y)的颜色
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public string GetColor(int x, int y)
    {
        int _size = OP_GetColor(_op, x, y, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetColor(_op, x, y, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取鼠标位置.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int GetCursorPos(out int x, out int y) => OP_GetCursorPos(_op, out x, out y);
    /// <summary>
    /// 获取指定的字库中的字符数量
    /// </summary>
    /// <param name="idx"></param>
    public int GetDictCount(int idx) => OP_GetDictCount(_op, idx);
    /// <summary>
    /// 获取顶层活动窗口中具有输入焦点的窗口句柄
    /// </summary>
    public int GetForegroundFocus() => OP_GetForegroundFocus(_op);
    /// <summary>
    /// 获取顶层活动窗口,可以获取到按键自带插件无法获取到的句柄
    /// </summary>
    public int GetForegroundWindow() => OP_GetForegroundWindow(_op);
    /// <summary>
    /// 返回当前对象的ID值，这个值对于每个对象是唯一存在的。可以用来判定两个对象是否一致
    /// </summary>
    public int GetID() => OP_GetID(_op);
    /// <summary>
    /// 获取指定的按键状态.(前台信息,不是后台)
    /// </summary>
    /// <param name="vk_code"></param>
    public int GetKeyState(int vk_code) => OP_GetKeyState(_op, vk_code);
    /// <summary>
    /// 
    /// </summary>
    public int GetLastError() => OP_GetLastError(_op);
    /// <summary>
    /// 获取鼠标指向的可见窗口句柄
    /// </summary>
    public int GetMousePointWindow() => OP_GetMousePointWindow(_op);
    /// <summary>
    /// 获取当前使用的字库序号
    /// </summary>
    public int GetNowDict() => OP_GetNowDict(_op);
    /// <summary>
    /// 获取目录
    /// </summary>
    public string GetPath()
    {
        int _size = OP_GetPath(_op, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetPath(_op, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取给定坐标的可见窗口句柄
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int GetPointWindow(int x, int y) => OP_GetPointWindow(_op, x, y);
    /// <summary>
    /// 根据指定的pid获取进程详细信息
    /// </summary>
    /// <param name="pid"></param>
    public string GetProcessInfo(int pid)
    {
        int _size = OP_GetProcessInfo(_op, pid, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetProcessInfo(_op, pid, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="data"></param>
    public int GetScreenData(int x1, int y1, int x2, int y2, ref IntPtr data) => OP_GetScreenData(_op, x1, y1, x2, y2, ref data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="data"></param>
    /// <param name="size"></param>
    public int GetScreenDataBmp(int x1, int y1, int x2, int y2, out IntPtr data, out int size) => OP_GetScreenDataBmp(_op, x1, y1, x2, y2, out data, out size);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame_id"></param>
    /// <param name="time"></param>
    public void GetScreenFrameInfo(ref int frame_id, ref int time) => OP_GetScreenFrameInfo(_op, ref frame_id, ref time);
    /// <summary>
    /// 获取特殊窗口
    /// </summary>
    /// <param name="flag"></param>
    public int GetSpecialWindow(int flag) => OP_GetSpecialWindow(_op, flag);
    /// <summary>
    /// 获取给定窗口相关的窗口句柄
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="flag"></param>
    public int GetWindow(int hwnd, int flag) => OP_GetWindow(_op, hwnd, flag);
    /// <summary>
    /// 获取窗口的类名
    /// </summary>
    /// <param name="hwnd"></param>
    public string GetWindowClass(int hwnd)
    {
        int _size = OP_GetWindowClass(_op, hwnd, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetWindowClass(_op, hwnd, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取指定窗口所在的进程ID
    /// </summary>
    /// <param name="hwnd"></param>
    public int GetWindowProcessId(int hwnd) => OP_GetWindowProcessId(_op, hwnd);
    /// <summary>
    /// 获取指定窗口所在的进程的exe文件全路径
    /// </summary>
    /// <param name="hwnd"></param>
    public string GetWindowProcessPath(int hwnd)
    {
        int _size = OP_GetWindowProcessPath(_op, hwnd, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetWindowProcessPath(_op, hwnd, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取窗口在屏幕上的位置
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    public int GetWindowRect(int hwnd, out int x1, out int y1, out int x2, out int y2) => OP_GetWindowRect(_op, hwnd, out x1, out y1, out x2, out y2);
    /// <summary>
    /// 获取指定窗口的一些属性
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="flag"></param>
    public int GetWindowState(int hwnd, int flag) => OP_GetWindowState(_op, hwnd, flag);
    /// <summary>
    /// 获取窗口的标题
    /// </summary>
    /// <param name="hwnd"></param>
    public string GetWindowTitle(int hwnd)
    {
        int _size = OP_GetWindowTitle(_op, hwnd, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetWindowTitle(_op, hwnd, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// Process
    /// inject dll
    /// </summary>
    /// <param name="process_name"></param>
    /// <param name="dll_name"></param>
    public int InjectDll(string process_name, string dll_name) => OP_InjectDll(_op, process_name, dll_name);
    /// <summary>
    /// 判定当前对象是否已绑定窗口.
    /// </summary>
    public int IsBind() => OP_IsBind(_op);
    /// <summary>
    /// 按住指定的虚拟键码
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyDown(int vk_code) => OP_KeyDown(_op, vk_code);
    /// <summary>
    /// 按住指定的虚拟键码
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyDownChar(string vk_code) => OP_KeyDownChar(_op, vk_code);
    /// <summary>
    /// 发送字符串
    /// long SendString(long HWND)
    /// 弹起来虚拟键vk_code
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyPress(int vk_code) => OP_KeyPress(_op, vk_code);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyPressChar(string vk_code) => OP_KeyPressChar(_op, vk_code);
    /// <summary>
    /// 根据指定的字符串序列，依次按顺序按下其中的字符
    /// </summary>
    /// <param name="key_str"></param>
    /// <param name="delay"></param>
    public int KeyPressStr(string key_str, int delay) => OP_KeyPressStr(_op, key_str, delay);
    /// <summary>
    /// 弹起来虚拟键vk_code
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyUp(int vk_code) => OP_KeyUp(_op, vk_code);
    /// <summary>
    /// 弹起来虚拟键vk_code
    /// </summary>
    /// <param name="vk_code"></param>
    public int KeyUpChar(string vk_code) => OP_KeyUpChar(_op, vk_code);
    /// <summary>
    /// 按下鼠标左键
    /// </summary>
    public int LeftClick() => OP_LeftClick(_op);
    /// <summary>
    /// 双击鼠标左键
    /// </summary>
    public int LeftDoubleClick() => OP_LeftDoubleClick(_op);
    /// <summary>
    /// 按住鼠标左键
    /// </summary>
    public int LeftDown() => OP_LeftDown(_op);
    /// <summary>
    /// 弹起鼠标左键
    /// </summary>
    public int LeftUp() => OP_LeftUp(_op);
    /// <summary>
    /// 从内存加载要查找的图片
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="data"></param>
    /// <param name="size"></param>
    public int LoadMemPic(string file_name, IntPtr data, int size) => OP_LoadMemPic(_op, file_name, data, size);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file_name"></param>
    public int LoadPic(string file_name) => OP_LoadPic(_op, file_name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pic_name"></param>
    public string MatchPicName(string pic_name)
    {
        int _size = OP_MatchPicName(_op, pic_name, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_MatchPicName(_op, pic_name, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 按下鼠标中键
    /// </summary>
    public int MiddleClick() => OP_MiddleClick(_op);
    /// <summary>
    /// 按住鼠标中键
    /// </summary>
    public int MiddleDown() => OP_MiddleDown(_op);
    /// <summary>
    /// 弹起鼠标中键
    /// </summary>
    public int MiddleUp() => OP_MiddleUp(_op);
    /// <summary>
    /// 鼠标相对于上次的位置移动rx,ry.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int MoveR(int x, int y) => OP_MoveR(_op, x, y);
    /// <summary>
    /// 把鼠标移动到目的点(x,y)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int MoveTo(int x, int y) => OP_MoveTo(_op, x, y);
    /// <summary>
    /// 把鼠标移动到目的范围内的任意一点
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public int MoveToEx(int x, int y, int w, int h) => OP_MoveToEx(_op, x, y, w, h);
    /// <summary>
    /// 移动指定窗口到指定位置
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int MoveWindow(int hwnd, int x, int y) => OP_MoveWindow(_op, hwnd, x, y);
    /// <summary>
    /// 识别屏幕范围(x1,y1,x2,y2)内符合color_format的字符串,并且相似度为sim,sim取值范围(0.1-1.0),
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    public string Ocr(int x1, int y1, int x2, int y2, string color, double sim)
    {
        int _size = OP_Ocr(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_Ocr(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 识别屏幕范围(x1,y1,x2,y2)内的字符串,自动二值化，而无需指定颜色
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="sim"></param>
    public string OcrAuto(int x1, int y1, int x2, int y2, double sim)
    {
        int _size = OP_OcrAuto(_op, x1, y1, x2, y2, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_OcrAuto(_op, x1, y1, x2, y2, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 从文件中识别图片,无需指定颜色
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="sim"></param>
    public string OcrAutoFromFile(string file_name, double sim)
    {
        int _size = OP_OcrAutoFromFile(_op, file_name, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_OcrAutoFromFile(_op, file_name, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 回识别到的字符串，以及每个字符的坐标.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <param name="sim"></param>
    public string OcrEx(int x1, int y1, int x2, int y2, string color, double sim)
    {
        int _size = OP_OcrEx(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_OcrEx(_op, x1, y1, x2, y2, color, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 从文件中识别图片
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="color_format"></param>
    /// <param name="sim"></param>
    public string OcrFromFile(string file_name, string color_format, double sim)
    {
        int _size = OP_OcrFromFile(_op, file_name, color_format, sim, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_OcrFromFile(_op, file_name, color_format, sim, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="address"></param>
    /// <param name="size"></param>
    public string ReadData(int hwnd, string address, int size)
    {
        int _size = OP_ReadData(_op, hwnd, address, size, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_ReadData(_op, hwnd, address, size, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 按下鼠标右键
    /// </summary>
    public int RightClick() => OP_RightClick(_op);
    /// <summary>
    /// 按住鼠标右键
    /// </summary>
    public int RightDown() => OP_RightDown(_op);
    /// <summary>
    /// 弹起鼠标右键
    /// </summary>
    public int RightUp() => OP_RightUp(_op);
    /// <summary>
    /// 运行可执行文件,可指定模式
    /// </summary>
    /// <param name="cmdline"></param>
    /// <param name="mode"></param>
    public int RunApp(string cmdline, int mode) => OP_RunApp(_op, cmdline, mode);
    /// <summary>
    /// 把屏幕坐标转换为窗口坐标
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public int ScreenToClient(int hwnd, ref int x, ref int y) => OP_ScreenToClient(_op, hwnd, ref x, ref y);
    /// <summary>
    /// 向指定窗口发送粘贴命令
    /// </summary>
    /// <param name="hwnd"></param>
    public int SendPaste(int hwnd) => OP_SendPaste(_op, hwnd);
    /// <summary>
    /// 向指定窗口发送文本数据
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="str"></param>
    public int SendString(int hwnd, string str) => OP_SendString(_op, hwnd, str);
    /// <summary>
    /// 向指定窗口发送文本数据-输入法
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="str"></param>
    public int SendStringIme(int hwnd, string str) => OP_SendStringIme(_op, hwnd, str);
    /// <summary>
    /// 设置窗口客户区域的宽度和高度
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="width"></param>
    /// <param name="hight"></param>
    public int SetClientSize(int hwnd, int width, int hight) => OP_SetClientSize(_op, hwnd, width, hight);
    /// <summary>
    /// 设置剪贴板数据
    /// </summary>
    /// <param name="str"></param>
    public int SetClipboard(string str) => OP_SetClipboard(_op, str);
    /// <summary>
    /// 设置字库文件
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="file_name"></param>
    public int SetDict(int idx, string file_name) => OP_SetDict(_op, idx, file_name);
    /// <summary>
    /// 设置图像输入方式，默认窗口截图
    /// </summary>
    /// <param name="mode"></param>
    public int SetDisplayInput(string mode) => OP_SetDisplayInput(_op, mode);
    /// <summary>
    /// 设置按键时,键盘按下和弹起的时间间隔
    /// </summary>
    /// <param name="type"></param>
    /// <param name="delay"></param>
    public int SetKeypadDelay(string type, int delay) => OP_SetKeypadDelay(_op, type, delay);
    /// <summary>
    /// 设置内存字库文件
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="data"></param>
    /// <param name="size"></param>
    public int SetMemDict(int idx, string data, int size) => OP_SetMemDict(_op, idx, data, size);
    /// <summary>
    /// 设置鼠标单击或者双击时,鼠标按下和弹起的时间间隔
    /// </summary>
    /// <param name="type"></param>
    /// <param name="delay"></param>
    public int SetMouseDelay(string type, int delay) => OP_SetMouseDelay(_op, type, delay);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path_of_engine"></param>
    /// <param name="dll_name"></param>
    /// <param name="argv"></param>
    public int SetOcrEngine(string path_of_engine, string dll_name, string argv) => OP_SetOcrEngine(_op, path_of_engine, dll_name, argv);
    /// <summary>
    /// 设置目录
    /// </summary>
    /// <param name="path"></param>
    public int SetPath(string path) => OP_SetPath(_op, path);
    /// <summary>
    /// 设置屏幕数据模式，0:从上到下(默认),1:从下到上
    /// </summary>
    /// <param name="mode"></param>
    public int SetScreenDataMode(int mode) => OP_SetScreenDataMode(_op, mode);
    /// <summary>
    /// 设置是否弹出错误信息,默认是打开 0:关闭，1:显示为信息框，2:保存到文件,3:输出到标准输出
    /// </summary>
    /// <param name="show_type"></param>
    public int SetShowErrorMsg(int show_type) => OP_SetShowErrorMsg(_op, show_type);
    /// <summary>
    /// 设置窗口的大小
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public int SetWindowSize(int hwnd, int width, int height) => OP_SetWindowSize(_op, hwnd, width, height);
    /// <summary>
    /// 设置窗口的状态
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="flag"></param>
    public int SetWindowState(int hwnd, int flag) => OP_SetWindowState(_op, hwnd, flag);
    /// <summary>
    /// 设置窗口的标题
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="title"></param>
    public int SetWindowText(int hwnd, string title) => OP_SetWindowText(_op, hwnd, title);
    /// <summary>
    /// 设置窗口的透明度
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="trans"></param>
    public int SetWindowTransparent(int hwnd, int trans) => OP_SetWindowTransparent(_op, hwnd, trans);
    /// <summary>
    /// sleep
    /// </summary>
    /// <param name="millseconds"></param>
    public int Sleep(int millseconds) => OP_Sleep(_op, millseconds);
    /// <summary>
    /// 解绑窗口
    /// </summary>
    public int UnBindWindow() => OP_UnBindWindow(_op);
    /// <summary>
    /// 使用哪个字库文件进行识别
    /// </summary>
    /// <param name="idx"></param>
    public int UseDict(int idx) => OP_UseDict(_op, idx);
    /// <summary>
    /// 1.版本号Version
    /// </summary>
    public string Ver()
    {
        int _size = OP_Ver(_op, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_Ver(_op, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 等待指定的按键按下 (前台,不是后台)
    /// </summary>
    /// <param name="vk_code"></param>
    /// <param name="time_out"></param>
    public int WaitKey(int vk_code, int time_out) => OP_WaitKey(_op, vk_code, time_out);
    /// <summary>
    /// 滚轮向下滚
    /// </summary>
    public int WheelDown() => OP_WheelDown(_op);
    /// <summary>
    /// 滚轮向上滚
    /// </summary>
    public int WheelUp() => OP_WheelUp(_op);
    /// <summary>
    /// 运行可执行文件，可指定显示模式
    /// </summary>
    /// <param name="cmdline"></param>
    /// <param name="cmdshow"></param>
    public int WinExec(string cmdline, int cmdshow) => OP_WinExec(_op, cmdline, cmdshow);
    /// <summary>
    /// 向某进程写入数据
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="address"></param>
    /// <param name="data"></param>
    /// <param name="size"></param>
    public int WriteData(int hwnd, string address, string data, int size) => OP_WriteData(_op, hwnd, address, data, size);
    #region DLL Import Define
    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr OP_CreateOP();

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void OP_ReleaseOP(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_AddDict(IntPtr _op, int idx, string dict_info);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_AStarFindPath(IntPtr _op, int mapWidth, int mapHeight, string disable_points, int beginX, int beginY, int endX, int endY, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_BindWindow(IntPtr _op, int hwnd, string display, string mouse, string keypad, int mode);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Capture(IntPtr _op, int x1, int y1, int x2, int y2, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_CapturePre(IntPtr _op, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_ClearDict(IntPtr _op, int idx);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_ClientToScreen(IntPtr _op, int ClientToScreen, ref int x, ref int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_CmpColor(IntPtr _op, int x, int y, string color, double sim);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Delay(IntPtr _op, int mis);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Delays(IntPtr _op, int mis_min, int mis_max);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnablePicCache(IntPtr _op, int enable);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumProcess(IntPtr _op, string name, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumWindow(IntPtr _op, int parent, string title, string class_name, int filter, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumWindowByProcess(IntPtr _op, string process_name, string title, string class_name, int filter, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FetchWord(IntPtr _op, int x1, int y1, int x2, int y2, string color, string word, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindColor(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, int dir, out int x, out int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindColorBlock(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, int count, int height, int width, out int x, out int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindColorBlockEx(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, int count, int height, int width, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindColorEx(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, int dir, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindLine(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindMultiColor(IntPtr _op, int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out int x, out int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindMultiColorEx(IntPtr _op, int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindNearestPos(IntPtr _op, string all_pos, int type, int x, int y, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindPic(IntPtr _op, int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, out int x, out int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindPicEx(IntPtr _op, int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindPicExS(IntPtr _op, int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindStr(IntPtr _op, int x1, int y1, int x2, int y2, string strs, string color, double sim, out int retx, out int rety);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindStrEx(IntPtr _op, int x1, int y1, int x2, int y2, string strs, string color, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindWindow(IntPtr _op, string class_name, string title);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindWindowByProcess(IntPtr _op, string process_name, string class_name, string title);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindWindowByProcessId(IntPtr _op, int process_id, string class_name, string title);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FindWindowEx(IntPtr _op, int parent, string class_name, string title);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_FreePic(IntPtr _op, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetBasePath(IntPtr _op, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetBindWindow(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetClientRect(IntPtr _op, int hwnd, out int x1, out int y1, out int x2, out int y2);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetClientSize(IntPtr _op, int hwnd, out int width, out int height);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetClipboard(IntPtr _op, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetCmdStr(IntPtr _op, string cmd, int millseconds, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetColor(IntPtr _op, int x, int y, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetCursorPos(IntPtr _op, out int x, out int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetDictCount(IntPtr _op, int idx);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetForegroundFocus(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetForegroundWindow(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetID(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetKeyState(IntPtr _op, int vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetLastError(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetMousePointWindow(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetNowDict(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetPath(IntPtr _op, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetPointWindow(IntPtr _op, int x, int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetProcessInfo(IntPtr _op, int pid, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetScreenData(IntPtr _op, int x1, int y1, int x2, int y2, ref IntPtr data);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetScreenDataBmp(IntPtr _op, int x1, int y1, int x2, int y2, out IntPtr data, out int size);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void OP_GetScreenFrameInfo(IntPtr _op, ref int frame_id, ref int time);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetSpecialWindow(IntPtr _op, int flag);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindow(IntPtr _op, int hwnd, int flag);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowClass(IntPtr _op, int hwnd, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowProcessId(IntPtr _op, int hwnd);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowProcessPath(IntPtr _op, int hwnd, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowRect(IntPtr _op, int hwnd, out int x1, out int y1, out int x2, out int y2);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowState(IntPtr _op, int hwnd, int flag);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_GetWindowTitle(IntPtr _op, int hwnd, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_InjectDll(IntPtr _op, string process_name, string dll_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_IsBind(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyDown(IntPtr _op, int vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyDownChar(IntPtr _op, string vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyPress(IntPtr _op, int vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyPressChar(IntPtr _op, string vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyPressStr(IntPtr _op, string key_str, int delay);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyUp(IntPtr _op, int vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_KeyUpChar(IntPtr _op, string vk_code);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LeftClick(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LeftDoubleClick(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LeftDown(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LeftUp(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LoadMemPic(IntPtr _op, string file_name, IntPtr data, int size);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_LoadPic(IntPtr _op, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MatchPicName(IntPtr _op, string pic_name, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MiddleClick(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MiddleDown(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MiddleUp(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MoveR(IntPtr _op, int x, int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MoveTo(IntPtr _op, int x, int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MoveToEx(IntPtr _op, int x, int y, int w, int h);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_MoveWindow(IntPtr _op, int hwnd, int x, int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Ocr(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_OcrAuto(IntPtr _op, int x1, int y1, int x2, int y2, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_OcrAutoFromFile(IntPtr _op, string file_name, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_OcrEx(IntPtr _op, int x1, int y1, int x2, int y2, string color, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_OcrFromFile(IntPtr _op, string file_name, string color_format, double sim, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_ReadData(IntPtr _op, int hwnd, string address, int size, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_RightClick(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_RightDown(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_RightUp(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_RunApp(IntPtr _op, string cmdline, int mode);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_ScreenToClient(IntPtr _op, int hwnd, ref int x, ref int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SendPaste(IntPtr _op, int hwnd);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SendString(IntPtr _op, int hwnd, string str);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SendStringIme(IntPtr _op, int hwnd, string str);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetClientSize(IntPtr _op, int hwnd, int width, int hight);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetClipboard(IntPtr _op, string str);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetDict(IntPtr _op, int idx, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetDisplayInput(IntPtr _op, string mode);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetKeypadDelay(IntPtr _op, string type, int delay);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetMemDict(IntPtr _op, int idx, string data, int size);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetMouseDelay(IntPtr _op, string type, int delay);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetOcrEngine(IntPtr _op, string path_of_engine, string dll_name, string argv);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetPath(IntPtr _op, string path);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetScreenDataMode(IntPtr _op, int mode);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetShowErrorMsg(IntPtr _op, int show_type);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetWindowSize(IntPtr _op, int hwnd, int width, int height);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetWindowState(IntPtr _op, int hwnd, int flag);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetWindowText(IntPtr _op, int hwnd, string title);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_SetWindowTransparent(IntPtr _op, int hwnd, int trans);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Sleep(IntPtr _op, int millseconds);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_UnBindWindow(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_UseDict(IntPtr _op, int idx);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Ver(IntPtr _op, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_WaitKey(IntPtr _op, int vk_code, int time_out);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_WheelDown(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_WheelUp(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_WinExec(IntPtr _op, string cmdline, int cmdshow);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_WriteData(IntPtr _op, int hwnd, string address, string data, int size);

    #endregion
}