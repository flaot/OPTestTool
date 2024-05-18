using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptTestTools.StaticData
{
    /// <summary>
    /// 综合设置静态类
    /// </summary>
    public static class Set
    {
        /// <summary>
        /// 0 正常模式(默认模式)
        /// 1 硬件模拟
        /// 2 硬件模拟2(ps2)（仅仅支持标准的3键鼠标，即左键，右键，中键，带滚轮的鼠标,2键和5键等扩展鼠标不支持）
        /// 3 硬件模拟3
        /// </summary>
        public static int SetSimMode = 0;

        /// <summary>
        /// 取值范围大于等于0  取值为0 表示关闭CPU优化. 这个值越大表示降低CPU效果越好.
        /// </summary>
        public static int DownCpuType = 0;
        public static int DownCpuRate = 0;
        /// <summary>
        /// 0 关闭锁定
        /// 1 开启锁定(键盘鼠标都锁定)
        /// 2 只锁定鼠标
        /// 3 只锁定键盘
        /// 4 同1,但当您发现某些特殊按键无法锁定时,比如(回车，ESC等)，那就用这个模式吧.但此模式会让SendString函数后台失效，或者采用和SendString类似原理发送字符串的其他3方函数失效.
        /// 5 同3,但当您发现某些特殊按键无法锁定时,比如(回车，ESC等)，那就用这个模式吧.但此模式会让SendString函数后台失效，或者采用和SendString类似原理发送字符串的其他3方函数失效.
        /// </summary>
        public static int LockInput = 0;

        /// <summary>
        /// 0 关闭
        /// 1 开启
        /// </summary>
        public static int EnableFakeActive = 0;

        /// <summary>
        /// 设置全局路径,设置了此路径后,所有接口调用中,相关的文件都相对于此路径. 比如图片,字库等.
        /// </summary>
        public static string SetPath = "";

        /// <summary>
        /// 取值范围大于0. 默认是1.0 表示不加速，也不减速. 小于1.0表示减速,大于1.0表示加速. 精度为小数点后1位. 也就是说1.5 和 1.56其实是一样的.
        /// </summary>
        public static double HackSpeed = 1.0;

        /// <summary>
        /// 针对部分检测措施的保护盾
        /// 0 表示关闭保护盾(仅仅对memory memory2 memory3 memory4 b2 b3起作用)
        /// 1 表示打开保护盾
        /// </summary>
        public static int DmGuardEnable = 0;

        /// <summary>
        /// "np" : 这个是防止NP检测
        /// "memory" : 这个保护内存系列接口和汇编接口可以正常运行.
        /// "memory2" : 这个保护内存系列接口和汇编接口可以正常运行
        /// "memory3 pid addr_start addr_end" : 这个保护内存系列接口和汇编接口可以正常运行.
        /// "memory4" : 这个保护内存系列接口和汇编接口可以正常运行.
        /// "display2" : 同display,但此模式用在一些极端的场合. 比如用任何截图软件也无法截图时，可以考虑这个盾
        /// "display3 <hwnd>" : 此盾可以保护当前进程指定的窗口(和子窗口)，无法被用正常手段截图. hwnd是必选参数. 并且必须是顶级窗口. 
        /// "block [pid]" : 保护指定进程不被非法访问. pid为可选参数.如果不指定pid，默认保护当前进程,另种实现方式.（
        /// "b2 [pid]" : 保护指定进程不被非法访问. pid为可选参数.如果不指定pid，默认保护当前进程,另种实现方式.(
        /// "b3 [pid]" : 保护指定进程不被非法访问. pid为可选参数.如果不指定pid，默认保护当前进程,另种实现方式.(
        /// "f1 [pid]" : 把当前进程伪装成pid指定的进程，可以保护进程路径无法被获取到.如果省略pid参数，则伪装成svchost.exe进程.
        /// "d1 [cls][add dll_name exact]" : 阻止指定的dll加载到本进程.这里的dll_name不区分大小写. 
        /// "f2 <target_process> <protect_process>" :把protect_process伪装成target_process运行. 此盾会加载target_process运行,然后用protect_process来替换target_process,从而达到伪装自身的目的.此盾不加载驱动. 这个protect_process也可以使用内存地址的形式，不用路径. 写法是这样<addr,size>,addr是内存地址,size是大小,都是10进制. 后面有例子 (使用此盾后，别人无法获取到你的进程的真实路径，但自己也同样无法获取，所以如果要获取真实路径，请务必在获取到路径后保存后,通过共享内存等方式传递给保护进程). 返回值为伪装后的进程ID
        /// "phide [pid]" : 隐藏指定进程,保护指定进程以及进程内的窗口不被非法访问. pid为可选参数.如果不指定pid，默认保护当前进程.(此模式需要加载驱动,普通版本仅支持32位系统)
        /// "phide2 [pid]" : 同phide. 只是进程不隐藏(可在任务管理器中操作) (此模式需要加载驱动,普通版本仅支持32位系统)
        /// "phide3 [pid]" : 只隐藏进程(在任务管理器看不到),但不保护进程和窗口. (此模式需要加载驱动,普通版本仅支持32位系统)
        /// "hm module unlink" : 防止当前进程中的指定模块被非法访问. module为模块名(为0表示EXE模块),比如dm.dll 。 unlink取0或者1，1表示是否把模块在进程模块链表中擦除,0表示不擦除.(此模式需要加载驱动) 
        /// "inject mode pid <param1> <param2>" : 注入指定的DLL到指定的进程中.mode表示注入模式.pid表示需要注入进去的进程ID.param1和param2参数含义根据mode决定.(此模式需要加载驱动)
        /// "del <path>" :强制删除指定的文件. path表示需要删除的文件的全路径. 当path为0时,表示为当前dm.dll的路径,当path为1时,表示为当前EXE的全路径.(此模式需要加载驱动)其它后续开发.
        /// "★ cl pid type name": 关闭指定进程中，对象名字中含有name的句柄. pid表示进程PID. type表示需要关闭的句柄类型. 比如Section Event Mutant等. 具体的类型可以用pchunter查看.
        /// </summary>
        public static string DmGuardType = "";
    }
}
