/**
This is an automatically generated class by OpExport. Please do not modify it.
License：https://github.com/WallBreaker2/op/blob/master/LICENSE 
**/

using System.Runtime.InteropServices;
public partial class OpSoft : IDisposable, IComparable<OpSoft>
{
    const string DLL_NAME = @"./Lib/op_x64.dll";

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
    /// 根据 A 星算法，获取地图上从源坐标到目的坐标的一条最短路径
    /// </summary>
    /// <param name="mapWidth">区域的左上 X 坐标</param>
    /// <param name="mapHeight">区域的左上 Y 坐标</param>
    /// <param name="disable_points">不可通行的坐标，以"|"分割，例如:"10,15 | 20,30"</param>
    /// <param name="beginX">源坐标 X</param>
    /// <param name="beginY">源坐标 Y</param>
    /// <param name="endX">目的坐标 X</param>
    /// <param name="endY">目的坐标 Y</param>
    /// <returns>找到的路径结果</returns>
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
    /// 蜂鸣器
    /// </summary>
    /// <param name="freq"></param>
    /// <param name="durationMs"></param>
    public int Beep(int freq, int durationMs) => OP_Beep(_op, freq, durationMs);
    /// <summary>
    /// 绑定指定的窗口,并指定这个窗口的屏幕颜色获取方式,鼠标仿真模式,键盘仿真模式,以及模式设定.
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="display">屏幕显示模式,取值定义如下 normal:正常模式,平常我们用的前台截屏模式 normal.dxgi:dxgi 截图模式，这个速度更快，更省 CPU 【注意：尚未发布无法使用】 gdi:gdi 模式,用于窗口采用 GDI 方式刷新时,此模式占用 CPU 较大 gdi2:gdi2 模式,此模式兼容性较强,但是速度比 gdi 模式要慢许多 dx:dx 模式,等同于 dx.d3d9 dx2:dx2 模式,用于窗口采用 dx 模式刷新 dx.d3d9:d3d9 模式,使用 d3d9 渲染 dx.d3d10:d3d10 模式,使用 d3d10 渲染 dx.d3d11:d3d11 模式,使用 d3d11 渲染 opengl:opengl 模式，使用 opengl 渲染的窗口 opengl.std:测试中 opengl.nox:opengl 模式，针对最新夜神模拟器的渲染方式，测试中... opengl.es:测试中... opengl.fi:测试中...</param>
    /// <param name="mouse">鼠标仿真模式,取值定义如下 normal:正常模式,平常我们用的前台鼠标模式 normal.hd: windows:Windows 模式,采取模拟 windows 消息方式 dx:dx 模式</param>
    /// <param name="keypad">键盘仿真模式,取值定义如下 normal:正常模式,平常我们用的前台键盘模式 normal.hd: windows:Windows 模式,采取模拟 windows 消息方式</param>
    /// <param name="mode">模式,取值 0、1</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int BindWindow(int hwnd, string display, string mouse, string keypad, int mode) => OP_BindWindow(_op, hwnd, display, mouse, keypad, mode);
    /// <summary>
    /// 抓取指定区域(x1, y1, x2, y2)的图像, 保存为文件
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="file_name">文件名,保存在 SetPath 中设置的目录，也可以自定义路径</param>
    /// <returns>0：失败 1：成功</returns>
    public int Capture(int x1, int y1, int x2, int y2, string file_name) => OP_Capture(_op, x1, y1, x2, y2, file_name);
    /// <summary>
    /// 取上次操作的图色区域，保存为 file(24 位位图)
    /// </summary>
    /// <param name="file_name">设置保存文件名,保存路径是 SetPath 设置的目录，也可以指定全路径</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int CapturePre(string file_name) => OP_CapturePre(_op, file_name);
    /// <summary>
    /// 把窗口坐标转换为屏幕坐标
    /// </summary>
    /// <param name="ClientToScreen">指定的窗口句柄</param>
    /// <param name="x">变参指针: 接收窗口 X 坐标</param>
    /// <param name="y">变参指针: 接收窗口 Y 坐标</param>
    /// <returns>0：表示操作失败。 1：表示操作成功。</returns>
    public int ClientToScreen(int ClientToScreen, ref int x, ref int y) => OP_ClientToScreen(_op, ClientToScreen, ref x, ref y);
    /// <summary>
    /// 比较指定坐标点(x,y)的颜色
    /// </summary>
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <param name="color">颜色字符串，例如"ffffff-202020|000000-000000"，，每种颜色用"|"分割，最多 10 种</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0 标</param>
    /// <returns>0：颜色不匹配 1：颜色匹配</returns>
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
    /// 关闭电源管理，不会进入睡眠
    /// </summary>
    public int DisablePowerSave() => OP_DisablePowerSave(_op);
    /// <summary>
    /// 关闭屏幕保护
    /// </summary>
    public int DisableScreenSave() => OP_DisableScreenSave(_op);
    /// <summary>
    /// 设置是否开启或者关闭插件内部的图片缓存机制
    /// </summary>
    /// <param name="enable">0：关闭,1:打开</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int EnablePicCache(int enable) => OP_EnablePicCache(_op, enable);
    /// <summary>
    /// 根据指定进程名,枚举系统中符合条件的进程 PID
    /// </summary>
    /// <param name="name">进程名称</param>
    /// <returns>返回所有匹配的进程 PID,返回格式："10180,15352,15000,17620,19412"</returns>
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
    /// <param name="parent">父窗口的句柄</param>
    /// <param name="title">窗口的标题</param>
    /// <param name="class_name">窗口的类名</param>
    /// <param name="filter">表示窗口的过滤条件,可以是以下值 1:匹配窗口标题，参数 title 有效 2:匹配窗口类名，参数 class_name 有效 4:只匹配指定父窗口的第一层子窗口 8:匹配所有者窗口为 0 的窗口，即顶级窗口 16:匹配可见的窗口 32:匹配出的窗口按照窗口打开顺序依次排列</param>
    /// <returns>返回所有匹配到的窗口句柄</returns>
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
    /// <param name="process_name">进程名称</param>
    /// <param name="title">窗口的标题</param>
    /// <param name="class_name">窗口的类名</param>
    /// <param name="filter">表示窗口的过滤条件,可以是以下值 1:匹配窗口标题，参数 title 有效 2:匹配窗口类名，参数 class_name 有效 4:只匹配指定父窗口的第一层子窗口 8:匹配所有者窗口为 0 的窗口，即顶级窗口 16:匹配可见的窗口 32:匹配出的窗口按照窗口打开顺序依次排列</param>
    /// <returns>返回所有匹配到的窗口句柄</returns>
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
    /// 查找指定区域内的颜色
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下</param>
    /// <param name="x">变参指针: 返回 X 坐标</param>
    /// <param name="y">变参指针: 返回 Y 坐标</param>
    /// <returns>0：未找到 1：成功找到</returns>
    public int FindColor(int x1, int y1, int x2, int y2, string color, double sim, int dir, out int x, out int y) => OP_FindColor(_op, x1, y1, x2, y2, color, sim, dir, out x, out y);
    /// <summary>
    /// 查找指定区域内的颜色块,颜色格式"RRGGBB-DRDGDB"
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="count">在宽度为 width,高度为 height 的颜色块中，符合 color 颜色的最小数量,通过工具在二值化区域中查看</param>
    /// <param name="height">颜色块的宽度</param>
    /// <param name="width">颜色块的高度</param>
    /// <param name="x">变参指针: 返回颜色块的左上角的 X 坐标</param>
    /// <param name="y">变参指针: 返回颜色块的左上角的 Y 坐标</param>
    /// <returns>0:找到 1:没找到</returns>
    public int FindColorBlock(int x1, int y1, int x2, int y2, string color, double sim, int count, int height, int width, out int x, out int y) => OP_FindColorBlock(_op, x1, y1, x2, y2, color, sim, count, height, width, out x, out y);
    /// <summary>
    /// 查找指定区域内的所有颜色块, 颜色格式"RRGGBB-DRDGDB"
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="count">在宽度为 width,高度为 height 的颜色块中，符合 color 颜色的最小数量,通过工具在二值化区域中查看</param>
    /// <param name="height">颜色块的宽度</param>
    /// <param name="width">颜色块的高度</param>
    /// <returns>返回所有颜色块信息的坐标</returns>
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
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下</param>
    /// <returns>返回所有颜色信息的坐标值</returns>
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
    /// 在指定的屏幕坐标范围内，查找指定颜色的直线
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的结果</returns>
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
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="first_color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="offset_color">偏移颜色可以支持任意多个点,格式为"x1|y1|RRGGBB-DRDGDB|RRGGBB-DRDGDB……</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下<a href="#dir">Dir</a> </param>
    /// <param name="x">变参指针: 返回 X 坐标, 坐标为 first_color 所在坐标</param>
    /// <param name="y">变参指针: 返回 Y 坐标, 坐标为 first_color 所在坐标</param>
    /// <returns>0：未找到 1：成功找到</returns>
    public int FindMultiColor(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out int x, out int y) => OP_FindMultiColor(_op, x1, y1, x2, y2, first_color, offset_color, sim, dir, out x, out y);
    /// <summary>
    /// 根据指定的多点查找所有颜色坐标
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="first_color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="offset_color">偏移颜色可以支持任意多个点,格式为"x1|y1|RRGGBB-DRDGDB|RRGGBB-DRDGDB……</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下<a href="#dir">Dir</a> </param>
    /// <returns>返回所有颜色信息的坐标,坐标是 first_color 所在的坐标</returns>
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
    /// 在一组位置中查找最近的位置
    /// </summary>
    /// <param name="all_pos">位置</param>
    /// <param name="type">类型</param>
    /// <param name="x">坐标 x</param>
    /// <param name="y">坐标 y</param>
    /// <returns>最接近指定坐标 (x, y) 的位置</returns>
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
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="files">图片名,可以是多个图片,比如"test1.bmp|test2.bmp|test3.bmp"</param>
    /// <param name="delta_color">颜色色偏,比如"203040"</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下<a href="#dir">Dir</a> </param>
    /// <param name="x">变参指针: 返回图片左上角的 X 坐标</param>
    /// <param name="y">变参指针: 返回图片左上角的 Y 坐标</param>
    /// <returns>返回找到的图片的序号,从 0 开始索引.如果没找到返回-1</returns>
    public int FindPic(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, out int x, out int y) => OP_FindPic(_op, x1, y1, x2, y2, files, delta_color, sim, dir, out x, out y);
    /// <summary>
    /// 查找多个图片,并且返回所有找到的图像的坐标
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="files">图片名,可以是多个图片,比如"test1.bmp|test2.bmp|test3.bmp"</param>
    /// <param name="delta_color">颜色色偏,比如"203040"</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下<a href="#dir">Dir</a> </param>
    /// <returns>返回的是所有找到的坐标格式如下:"id,x,y|id,x,y..|id,x,y";id 对应图片序号，x,y 图片左上角的坐标</returns>
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
    /// 这个函数可以查找多个图片, 并且返回所有找到的图像的坐标
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="files">图片名,可以是多个图片,比如"test1.bmp|test2.bmp|test3.bmp"</param>
    /// <param name="delta_color">颜色色偏,比如"203040"</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="dir">查找方向,取值如下<a href="#dir">Dir</a> </param>
    /// <returns>返回的是所有找到的坐标格式如下:"file,x,y| file,x,y..| file,x,y" (图片左上角的坐标)</returns>
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
    /// 在屏幕范围(x1,y1,x2,y2)内,查找 string(可以是任意个字符串的组合),并返回符合 color_format 的坐标位置
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="strs">待查找的字符串,可以是字符串组合，比如"长安|洛阳|大雁塔",中间用"|"来分割</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <param name="retx">变参指针: 返回 X 坐标没找到返回-1</param>
    /// <param name="rety">变参指针: 返回 Y 坐标没找到返回-1</param>
    /// <returns>返回字符串的索引 没找到返回-1, 比如"长安|洛阳",若找到长安，则返回 0</returns>
    public int FindStr(int x1, int y1, int x2, int y2, string strs, string color, double sim, out int retx, out int rety) => OP_FindStr(_op, x1, y1, x2, y2, strs, color, sim, out retx, out rety);
    /// <summary>
    /// 在屏幕范围(x1,y1,x2,y2)内,查找 string(可以是任意字符串的组合)
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="strs">待查找的字符串,可以是字符串组合，比如"伤害|攻击|血量",中间用"|"来分割</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回所有找到的坐标集合,格式如下: "id,x0,y0|id,x1,y1|......|id,xn,yn" 比如"0,100,20|2,30,40" 表示找到了两个,第一个,对应的是序号为 0 的字符串,坐标是(100,20),第二个是序号为 2 的字符串,坐标(30,40)</returns>
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
    /// <param name="class_name">窗口类名,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <param name="title">窗口标题,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <returns>返回窗口句柄,没找到则返回 0</returns>
    public int FindWindow(string class_name, string title) => OP_FindWindow(_op, class_name, title);
    /// <summary>
    /// 根据指定的进程名字，来查找可见窗口
    /// </summary>
    /// <param name="process_name">进程名,比如(notepad.exe),这里是精确匹配,但不区分大小写</param>
    /// <param name="class_name">窗口类名,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <param name="title">窗口标题,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <returns>返回窗口句柄,没找到则返回 0</returns>
    public int FindWindowByProcess(string process_name, string class_name, string title) => OP_FindWindowByProcess(_op, process_name, class_name, title);
    /// <summary>
    /// 根据指定的进程 Id，来查找可见窗口
    /// </summary>
    /// <param name="process_id">进程 id</param>
    /// <param name="class_name">窗口类名,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <param name="title">窗口标题,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <returns>返回窗口句柄,没找到则返回 0</returns>
    public int FindWindowByProcessId(int process_id, string class_name, string title) => OP_FindWindowByProcessId(_op, process_id, class_name, title);
    /// <summary>
    /// 查找符合类名或者标题名的顶层可见窗口,如果指定了 parent,则在 parent 的第一层子窗口中查找
    /// </summary>
    /// <param name="parent">父窗口句柄，如果为空，则匹配所有顶层窗口</param>
    /// <param name="class_name">窗口类名,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <param name="title">窗口标题,如果为空,则匹配所有,这里的匹配是模糊匹配</param>
    /// <returns>返回窗口句柄,没找到则返回 0</returns>
    public int FindWindowEx(int parent, string class_name, string title) => OP_FindWindowEx(_op, parent, class_name, title);
    /// <summary>
    /// 释放指定的图片
    /// </summary>
    /// <param name="file_name">文件名</param>
    /// <returns>0：失败 1：成功</returns>
    public int FreePic(string file_name) => OP_FreePic(_op, file_name);
    /// <summary>
    /// 获取插件目录
    /// </summary>
    /// <returns>返回当前插件所在路径</returns>
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
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="x1">变参指针: 返回窗口客户区左上角 X 坐标</param>
    /// <param name="y1">变参指针: 返回窗口客户区左上角 Y 坐标</param>
    /// <param name="x2">变参指针: 返回窗口客户区右下角 X 坐标</param>
    /// <param name="y2">变参指针: 返回窗口客户区右下角 Y 坐标</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int GetClientRect(int hwnd, out int x1, out int y1, out int x2, out int y2) => OP_GetClientRect(_op, hwnd, out x1, out y1, out x2, out y2);
    /// <summary>
    /// 获取窗口客户区域的宽度和高度
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="width">变参指针: 窗口宽度</param>
    /// <param name="height">变参指针: 窗口高度</param>
    /// <returns>0: 失败 1: 成功</returns>
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
    /// <param name="cmd">指定的可执行程序全路径</param>
    /// <param name="millseconds">等待的时间(毫秒)</param>
    /// <returns>cmd 输出的字符</returns>
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
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <returns>返回颜色字符串</returns>
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
    /// 获取鼠标位置
    /// </summary>
    /// <param name="x">变参指针: 返回 X 坐标</param>
    /// <param name="y">变参指针: 返回 Y 坐标</param>
    /// <returns>0：失败 1：成功</returns>
    public int GetCursorPos(out int x, out int y) => OP_GetCursorPos(_op, out x, out y);
    /// <summary>
    /// 得到系统的路径
    /// </summary>
    /// <param name="type"></param>
    public string GetDir(int type)
    {
        int _size = OP_GetDir(_op, type, _pStr, _nSize);
        if (_size > 0)
        {
            if (_nSize > 0) Marshal.FreeHGlobal(_pStr);
            _pStr = Marshal.AllocHGlobal(_nSize = _size);
            OP_GetDir(_op, type, _pStr, _nSize);
        }
        string str = Marshal.PtrToStringUni(_pStr);
        return str;
    }
    /// <summary>
    /// 获取顶层活动窗口中具有输入焦点的窗口句柄
    /// </summary>
    /// <returns>返回窗口句柄</returns>
    public int GetForegroundFocus() => OP_GetForegroundFocus(_op);
    /// <summary>
    /// 获取顶层活动窗口,可以获取到按键自带插件无法获取到的句柄
    /// </summary>
    /// <returns>返回窗口句柄</returns>
    public int GetForegroundWindow() => OP_GetForegroundWindow(_op);
    /// <summary>
    /// 返回当前对象的 ID 值，这个值对于每个对象是唯一存在的。可以用来判定两个对象是否一致
    /// </summary>
    /// <returns>当前对象的 ID 值.</returns>
    public int GetID() => OP_GetID(_op);
    /// <summary>
    /// 获取指定的按键状态.(前台信息,不是后台)
    /// </summary>
    /// <param name="vk_code">虚拟按键码</param>
    /// <returns>0：失败 1：成功</returns>
    public int GetKeyState(int vk_code) => OP_GetKeyState(_op, vk_code);
    /// <summary>
    /// 获取最后的错误
    /// </summary>
    /// <returns>0: 表示无错误</returns>
    public int GetLastError() => OP_GetLastError(_op);
    /// <summary>
    /// 获取鼠标指向的可见窗口句柄,可以获取到按键自带的插件无法获取到的句柄
    /// </summary>
    /// <returns>返回窗口句柄</returns>
    public int GetMousePointWindow() => OP_GetMousePointWindow(_op);
    /// <summary>
    /// 获取全局路径
    /// </summary>
    /// <returns>返回当前设置的全局路径</returns>
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
    /// <param name="x">屏幕 X 坐标</param>
    /// <param name="y">屏幕 Y 坐标</param>
    /// <returns>返回窗口句柄</returns>
    public int GetPointWindow(int x, int y) => OP_GetPointWindow(_op, x, y);
    /// <summary>
    /// 根据指定的 pid 获取进程详细信息,(进程名,进程全路径,CPU 占用率(百分比),内存占用量(字节))
    /// </summary>
    /// <param name="pid">进程 pid</param>
    /// <returns>返回格式"进程名|进程路径|cpu|内存"</returns>
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
    /// 获取指定区域的图像,用二进制数据的方式返回
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="data"></param>
    /// <returns>返回的是指定区域的二进制图片颜色数据，每个颜色是 4 个字节,表示方式为(00RRGGBB)</returns>
    public int GetScreenData(int x1, int y1, int x2, int y2, ref IntPtr data) => OP_GetScreenData(_op, x1, y1, x2, y2, ref data);
    /// <summary>
    /// 获取指定区域的图像,用 24 位位图的数据格式返回
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="data">变参指针:返回图片的数据指针</param>
    /// <param name="size">变参指针:返回图片的数据长度</param>
    /// <returns>0：失败 1：成功</returns>
    public int GetScreenDataBmp(int x1, int y1, int x2, int y2, out IntPtr data, out int size) => OP_GetScreenDataBmp(_op, x1, y1, x2, y2, out data, out size);
    /// <summary>
    /// 获取屏幕帧信息,刚方法未导出到 com 库
    /// </summary>
    /// <param name="frame_id">屏幕帧的 ID</param>
    /// <param name="time"></param>
    /// <returns>0：失败 1：成功</returns>
    public void GetScreenFrameInfo(ref int frame_id, ref int time) => OP_GetScreenFrameInfo(_op, ref frame_id, ref time);
    /// <summary>
    /// 获取特殊窗口
    /// </summary>
    /// <param name="flag">取值如下 0:获取桌面窗口 1:获取任务栏窗口</param>
    /// <returns>返回窗口句柄</returns>
    public int GetSpecialWindow(int flag) => OP_GetSpecialWindow(_op, flag);
    /// <summary>
    /// 获取给定窗口相关的窗口句柄
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="flag">取值如下 0:获取父窗口 1:获取第一个儿子窗口 2:获取 First 窗口 3:获取 Last 窗口 4:获取下一个窗口 5:获取上一个窗口 6:获取拥有者窗口 7:获取顶层窗口</param>
    /// <returns>返回窗口句柄</returns>
    public int GetWindow(int hwnd, int flag) => OP_GetWindow(_op, hwnd, flag);
    /// <summary>
    /// 获取窗口的类名
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <returns>窗口的类名</returns>
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
    /// 获取指定窗口所在的进程 ID
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <returns>返回进程 ID</returns>
    public int GetWindowProcessId(int hwnd) => OP_GetWindowProcessId(_op, hwnd);
    /// <summary>
    /// 获取指定窗口所在的进程的 exe 文件全路径
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <returns>返回进程所在的全路径</returns>
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
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="x1">变参指针: 返回窗口左上角 X 坐标</param>
    /// <param name="y1">变参指针: 返回窗口左上角 Y 坐标</param>
    /// <param name="x2">变参指针: 返回窗口右下角 X 坐标</param>
    /// <param name="y2">变参指针: 返回窗口右下角 Y 坐标</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int GetWindowRect(int hwnd, out int x1, out int y1, out int x2, out int y2) => OP_GetWindowRect(_op, hwnd, out x1, out y1, out x2, out y2);
    /// <summary>
    /// 获取指定窗口的一些属性
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="flag">取值如下 0:判断窗口是否存在 1:判断窗口是否处于激活 2:判断窗口是否可见 3:判断窗口是否最小化 4:判断窗口是否最大化 5:判断窗口是否置顶 6:判断窗口是否无响应 7:判断窗口是否可用(灰色为不可用)</param>
    /// <returns>0: 不满足条件 1: 满足条件</returns>
    public int GetWindowState(int hwnd, int flag) => OP_GetWindowState(_op, hwnd, flag);
    /// <summary>
    /// 获取窗口的标题
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <returns>返回窗口的标题</returns>
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
    /// 将指定的 DLL 注入到指定的进程中
    /// </summary>
    /// <param name="process_name">指定要注入 DLL 的进程名称</param>
    /// <param name="dll_name">注入的 DLL 名称</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int InjectDll(string process_name, string dll_name) => OP_InjectDll(_op, process_name, dll_name);
    /// <summary>
    /// 判断当前系统是否是64位操作系统
    /// </summary>
    public int Is64Bit() => OP_Is64Bit(_op);
    /// <summary>
    /// 判定当前对象是否已绑定窗口.
    /// </summary>
    public int IsBind() => OP_IsBind(_op);
    /// <summary>
    /// 按住指定的虚拟键码
    /// </summary>
    /// <param name="vk_code">虚拟按键码</param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyDown(int vk_code) => OP_KeyDown(_op, vk_code);
    /// <summary>
    /// 按住指定的虚拟键码,字符串形式
    /// </summary>
    /// <param name="vk_code">字符串描述的键码,大小写无所谓，按键具体对应关系<a href="#keycode">按键码</a> </param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyDownChar(string vk_code) => OP_KeyDownChar(_op, vk_code);
    /// <summary>
    /// 按住指定的虚拟键码
    /// </summary>
    /// <param name="vk_code">虚拟按键码</param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyPress(int vk_code) => OP_KeyPress(_op, vk_code);
    /// <summary>
    /// 按住指定的虚拟键码,字符串形式
    /// </summary>
    /// <param name="vk_code">字符串描述的键码,大小写无所谓，按键具体对应关系查看<a href="#keycode">按键码</a> </param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyPressChar(string vk_code) => OP_KeyPressChar(_op, vk_code);
    /// <summary>
    /// 根据指定的字符串序列，依次按顺序按下其中的字符
    /// </summary>
    /// <param name="key_str"></param>
    /// <param name="delay"></param>
    public int KeyPressStr(string key_str, int delay) => OP_KeyPressStr(_op, key_str, delay);
    /// <summary>
    /// 弹起来虚拟键 vk_code
    /// </summary>
    /// <param name="vk_code">虚拟按键码</param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyUp(int vk_code) => OP_KeyUp(_op, vk_code);
    /// <summary>
    /// 弹起来虚拟键,字符串形式
    /// </summary>
    /// <param name="vk_code">字符串描述的键码,大小写无所谓，按键具体对应关系查看<a href="#keycode">按键码</a> </param>
    /// <returns>0：失败 1：成功</returns>
    public int KeyUpChar(string vk_code) => OP_KeyUpChar(_op, vk_code);
    /// <summary>
    /// 按下鼠标左键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int LeftClick() => OP_LeftClick(_op);
    /// <summary>
    /// 双击鼠标左键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int LeftDoubleClick() => OP_LeftDoubleClick(_op);
    /// <summary>
    /// 按住鼠标左键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int LeftDown() => OP_LeftDown(_op);
    /// <summary>
    /// 弹起鼠标左键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int LeftUp() => OP_LeftUp(_op);
    /// <summary>
    /// 从内存加载要查找的图片
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="data"></param>
    /// <param name="size"></param>
    public int LoadMemPic(string file_name, IntPtr data, int size) => OP_LoadMemPic(_op, file_name, data, size);
    /// <summary>
    /// 预加载指定的图片加入缓存
    /// </summary>
    /// <param name="file_name">文件名,比如"1.bmp|2.bmp|3.bmp" 等.</param>
    /// <returns>0：失败 1：成功</returns>
    public int LoadPic(string file_name) => OP_LoadPic(_op, file_name);
    /// <summary>
    /// 根据通配符获取文件集合. 方便用于 FindPic 和 FindPicEx
    /// </summary>
    /// <param name="pic_name">文件名,比如"1.bmp|2.bmp|3.bmp" 等</param>
    /// <returns>返回的是通配符对应的文件集合，每个图片以|分割</returns>
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
    /// <returns>0：失败 1：成功</returns>
    public int MiddleClick() => OP_MiddleClick(_op);
    /// <summary>
    /// 按住鼠标中键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int MiddleDown() => OP_MiddleDown(_op);
    /// <summary>
    /// 弹起鼠标中键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int MiddleUp() => OP_MiddleUp(_op);
    /// <summary>
    /// 鼠标相对于上次的位置移动 rx,ry.
    /// </summary>
    /// <param name="x">相对于上次的 X 偏移</param>
    /// <param name="y">相对于上次的 Y 偏移</param>
    /// <returns>0：失败 1：成功</returns>
    public int MoveR(int x, int y) => OP_MoveR(_op, x, y);
    /// <summary>
    /// 把鼠标移动到目的点(x,y)
    /// </summary>
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <returns>0：失败 1：成功</returns>
    public int MoveTo(int x, int y) => OP_MoveTo(_op, x, y);
    /// <summary>
    /// 把鼠标移动到目的范围内的任意一点
    /// </summary>
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <param name="w">宽度(从 x 计算起)</param>
    /// <param name="h">高度(从 y 计算起)</param>
    /// <returns>返回要移动到的目标点. 格式为 x,y. 比如 MoveToEx 100,100,10,10,返回值可能是 101,102</returns>
    public int MoveToEx(int x, int y, int w, int h) => OP_MoveToEx(_op, x, y, w, h);
    /// <summary>
    /// 移动指定窗口到指定位置
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="x">指定的 X 坐标</param>
    /// <param name="y">指定的 Y 坐标</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int MoveWindow(int hwnd, int x, int y) => OP_MoveWindow(_op, hwnd, x, y);
    /// <summary>
    /// 识别屏幕范围(x1,y1,x2,y2)内符合 color_format 的字符串
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的字符串</returns>
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
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的字符串</returns>
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
    /// 从文件中识别图片,自动二值化,无需指定颜色
    /// </summary>
    /// <param name="file_name">文件名</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的字符串</returns>
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
    /// 该方法可以返回识别到的字符串，以及每个字符的坐标
    /// </summary>
    /// <param name="x1">区域的左上 X 坐标</param>
    /// <param name="y1">区域的左上 Y 坐标</param>
    /// <param name="x2">区域的右下 X 坐标</param>
    /// <param name="y2">区域的右下 Y 坐标</param>
    /// <param name="color">颜色格式串，比如"FFFFFF-000000|CCCCCC-000000"每种颜色用"|"分割</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的字符串以及坐标</returns>
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
    /// <param name="file_name">文件名</param>
    /// <param name="color_format">颜色格式串</param>
    /// <param name="sim">相似度,取值范围 0.1-1.0</param>
    /// <returns>返回识别到的字符串</returns>
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
    /// <param name="hwnd">窗口句柄，用于指定要从哪个窗口内读取数据</param>
    /// <param name="address">表示要读取数据的地址</param>
    /// <param name="size">要读取的数据的大小</param>
    /// <returns>读取到的数值</returns>
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
    /// <returns>0：失败 1：成功</returns>
    public int RightClick() => OP_RightClick(_op);
    /// <summary>
    /// 按住鼠标右键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int RightDown() => OP_RightDown(_op);
    /// <summary>
    /// 弹起鼠标右键
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int RightUp() => OP_RightUp(_op);
    /// <summary>
    /// 运行可执行文件,可指定模式
    /// </summary>
    /// <param name="cmdline">指定的可执行程序全路径</param>
    /// <param name="mode">取值如下 0:普通模式 1:加强模式</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int RunApp(string cmdline, int mode) => OP_RunApp(_op, cmdline, mode);
    /// <summary>
    /// 把屏幕坐标转换为窗口坐标
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="x">变参指针: 屏幕 X 坐标</param>
    /// <param name="y">变参指针: 屏幕 Y 坐标</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int ScreenToClient(int hwnd, ref int x, ref int y) => OP_ScreenToClient(_op, hwnd, ref x, ref y);
    /// <summary>
    /// 向指定窗口发送粘贴命令
    /// </summary>
    /// <param name="hwnd"></param>
    public int SendPaste(int hwnd) => OP_SendPaste(_op, hwnd);
    /// <summary>
    /// 向指定窗口发送文本数据
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="str">发送的文本数据</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SendString(int hwnd, string str) => OP_SendString(_op, hwnd, str);
    /// <summary>
    /// 向指定窗口发送文本数据-输入法
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="str">发送的文本数据</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SendStringIme(int hwnd, string str) => OP_SendStringIme(_op, hwnd, str);
    /// <summary>
    /// 设置窗口客户区域的宽度和高度
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="width">宽度</param>
    /// <param name="hight">高度</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SetClientSize(int hwnd, int width, int hight) => OP_SetClientSize(_op, hwnd, width, hight);
    /// <summary>
    /// 设置剪贴板数据
    /// </summary>
    /// <param name="str"></param>
    public int SetClipboard(string str) => OP_SetClipboard(_op, str);
    /// <summary>
    /// 设置字库文件(index：范围:0-9)不能超过 10 个字库
    /// </summary>
    /// <param name="idx">字库的序号,取值为 0-9</param>
    /// <param name="file_name">字库文件名</param>
    /// <returns>0：失败 1：成功</returns>
    public int SetDict(int idx, string file_name) => OP_SetDict(_op, idx, file_name);
    /// <summary>
    /// 设置图像输入方式，默认窗口截图
    /// </summary>
    /// <param name="mode">图色输入模式 screen:默认的模式，表示使用显示器或者后台窗口 pic:指定输入模式为指定的图片,可以是相对路径,相对于 SetPath 的路径：pic:test.bmp,也可以是绝对路径: pic:d:\test\test.bmp mem:指定输入模式为指定的图片,此图片在内存当中，格式：mem:addr 例如：mem:1230434</param>
    /// <returns>0：失败 1：成功</returns>
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
    /// <param name="idx">字库的序号</param>
    /// <param name="data">字库内容数据</param>
    /// <param name="size">字库大小</param>
    /// <returns>0：失败 1：成功</returns>
    public int SetMemDict(int idx, string data, int size) => OP_SetMemDict(_op, idx, data, size);
    /// <summary>
    /// 设置鼠标单击或者双击时,鼠标按下和弹起的时间间隔
    /// </summary>
    /// <param name="type"></param>
    /// <param name="delay"></param>
    public int SetMouseDelay(string type, int delay) => OP_SetMouseDelay(_op, type, delay);
    /// <summary>
    /// 设置 OCR 引擎
    /// </summary>
    /// <param name="path_of_engine">指定 OCR 引擎的路径</param>
    /// <param name="dll_name">OCR 引擎 DLL 名称</param>
    /// <param name="argv"></param>
    /// <returns>0：失败 1：成功</returns>
    public int SetOcrEngine(string path_of_engine, string dll_name, string argv) => OP_SetOcrEngine(_op, path_of_engine, dll_name, argv);
    /// <summary>
    /// 设置全局路径,设置了此路径后,所有接口调用中,相关的文件都相对于此路径. 比如图片,字库等.
    /// </summary>
    /// <param name="path">指定的路径。</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int SetPath(string path) => OP_SetPath(_op, path);
    /// <summary>
    /// 设置屏幕数据模式
    /// </summary>
    /// <param name="mode">0:从上到下(默认),1:从下到上</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int SetScreenDataMode(int mode) => OP_SetScreenDataMode(_op, mode);
    /// <summary>
    /// 设置是否弹出错误信息,默认是打开
    /// </summary>
    /// <param name="show_type">0:关闭，1:显示为信息框，2:保存到文件,3:输出到标准输出</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int SetShowErrorMsg(int show_type) => OP_SetShowErrorMsg(_op, show_type);
    /// <summary>
    /// 设置窗口客户区域的宽度和高度
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SetWindowSize(int hwnd, int width, int height) => OP_SetWindowSize(_op, hwnd, width, height);
    /// <summary>
    /// 设置窗口的状态
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="flag">取值定义如下 0:关闭指定窗口 1:激活指定窗口 2:最小化指定窗口,但不激活 3:最小化指定窗口,并释放内存,但同时也会激活窗口 4:最大化指定窗口,同时激活窗口 5:恢复指定窗口 ,但不激活 6:隐藏指定窗口 7:显示指定窗口 8:置顶指定窗口 9:取消置顶指定窗口 10:禁止指定窗口 11:取消禁止指定窗口 12:恢复并激活指定窗口 13:强制结束窗口所在进程 14:闪烁指定的窗口 15:使指定的窗口获取输入焦点</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SetWindowState(int hwnd, int flag) => OP_SetWindowState(_op, hwnd, flag);
    /// <summary>
    /// 设置窗口的标题
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="title">标题</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SetWindowText(int hwnd, string title) => OP_SetWindowText(_op, hwnd, title);
    /// <summary>
    /// 设置窗口的透明度
    /// </summary>
    /// <param name="hwnd">指定的窗口句柄</param>
    /// <param name="trans">透明度取值(0-255) 越小透明度越大 0 为完全透明(不可见) 255 为完全显示(不透明)</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int SetWindowTransparent(int hwnd, int trans) => OP_SetWindowTransparent(_op, hwnd, trans);
    /// <summary>
    /// 设置休眠时间
    /// </summary>
    /// <param name="millseconds">休眠时间(毫秒)</param>
    /// <returns>0：表示操作失败 1：表示操作成功</returns>
    public int Sleep(int millseconds) => OP_Sleep(_op, millseconds);
    /// <summary>
    /// 解除绑定窗口,并释放系统资源
    /// </summary>
    /// <returns>0: 失败 1: 成功</returns>
    public int UnBindWindow() => OP_UnBindWindow(_op);
    /// <summary>
    /// 选择使用哪个字库文件进行识别(index：范围:0-9)
    /// </summary>
    /// <param name="idx">字库的序号</param>
    /// <returns>0：失败 1：成功</returns>
    public int UseDict(int idx) => OP_UseDict(_op, idx);
    /// <summary>
    /// 获取当前 op 插件的版本号
    /// </summary>
    /// <returns>返回 op 插件的版本号</returns>
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
    /// <param name="vk_code">虚拟按键码,当此值为：0，表示等待任意按键。 鼠标左键是：1,鼠标右键时：2,鼠标中键是：4</param>
    /// <param name="time_out">等待多久,单位毫秒. 如果是 0，表示一直等待</param>
    /// <returns>0：失败 1：成功</returns>
    public int WaitKey(int vk_code, int time_out) => OP_WaitKey(_op, vk_code, time_out);
    /// <summary>
    /// 滚轮向下滚
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int WheelDown() => OP_WheelDown(_op);
    /// <summary>
    /// 滚轮向下滚
    /// </summary>
    /// <returns>0：失败 1：成功</returns>
    public int WheelUp() => OP_WheelUp(_op);
    /// <summary>
    /// 运行可执行文件，可指定显示模式
    /// </summary>
    /// <param name="cmdline">指定的可执行程序全路径</param>
    /// <param name="cmdshow">取值如下 0:隐藏 1:用最近的大小和位置显示,激活</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int WinExec(string cmdline, int cmdshow) => OP_WinExec(_op, cmdline, cmdshow);
    /// <summary>
    /// 向某进程写入数据
    /// </summary>
    /// <param name="hwnd">窗口句柄，用于指定要在哪个窗口内写入数据</param>
    /// <param name="address">写入数据的地址</param>
    /// <param name="data">写入的数据</param>
    /// <param name="size">写入的数据的大小</param>
    /// <returns>0: 失败 1: 成功</returns>
    public int WriteData(int hwnd, string address, string data, int size) => OP_WriteData(_op, hwnd, address, data, size);
    #region DLL Import Define
    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr OP_CreateOP();

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void OP_ReleaseOP(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_AStarFindPath(IntPtr _op, int mapWidth, int mapHeight, string disable_points, int beginX, int beginY, int endX, int endY, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Beep(IntPtr _op, int freq, int durationMs);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_BindWindow(IntPtr _op, int hwnd, string display, string mouse, string keypad, int mode);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Capture(IntPtr _op, int x1, int y1, int x2, int y2, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_CapturePre(IntPtr _op, string file_name);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_ClientToScreen(IntPtr _op, int ClientToScreen, ref int x, ref int y);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_CmpColor(IntPtr _op, int x, int y, string color, double sim);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Delay(IntPtr _op, int mis);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_Delays(IntPtr _op, int mis_min, int mis_max);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_DisablePowerSave(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_DisableScreenSave(IntPtr _op);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnablePicCache(IntPtr _op, int enable);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumProcess(IntPtr _op, string name, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumWindow(IntPtr _op, int parent, string title, string class_name, int filter, IntPtr _pStr, int _nSize);

    [DllImport(DLL_NAME, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern int OP_EnumWindowByProcess(IntPtr _op, string process_name, string title, string class_name, int filter, IntPtr _pStr, int _nSize);

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
    private static extern int OP_GetDir(IntPtr _op, int type, IntPtr _pStr, int _nSize);

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
    private static extern int OP_Is64Bit(IntPtr _op);

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