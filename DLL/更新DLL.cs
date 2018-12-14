// 自动选择最新的文件源
var di = ".".AsDirectory();
var srcs = new String[] { @"..\..\Bin", @"C:\X\DLL", @"C:\X\Bin", @"D:\X\Bin", @"E:\X\DLL", @"E:\X\Bin" };
di.CopyIfNewer(srcs, "*.dll;*.exe;*.xml;*.pdb;*.cs");
