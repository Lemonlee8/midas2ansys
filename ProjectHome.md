## 源起 ##
　　MIDAS主要以建模方便快捷见长，且符有截面验算和优化功能，而ANSYS则是久经考验的计算专家，以计算准确见长。如果在结构设计过程中，想对MIDAS的计算结果进行验证，选用ANSYS无疑是个很好选择。本软件就是借助ANSYS的参数化建模功能，使用户快速把MIDAS模型转化到 ANSYS中去，节约不必要的人力和时间浪费。
## 界面 ##
[查看](http://lubanrenpics.appspot.com/image/49001/)

## 主要特点 ##
  1. 支持MIDAS中Beam单元的转化，输出ANSYS时可选Beam44,Beam188和Beam189；
  1. MIDAS中的单元Beta角自动转化为ANSYS中的单元keypoint；
  1. 支持MIDAS中用户自定义截面（H型、T型、圆型、矩型、箱型）的转化；
  1. 模型类库与主程序剥离，方便代码重用；
  1. 完全开放源代码,允许自由定制；
## 更新发布记录 ##
  * _Midas2ANSYS\_V1.0.0.58_ `[2011-01-29]`　　[下载](http://code.google.com/p/midas2ansys/downloads/detail?name=Midas2Ansys_V1.0.0.58.rar&can=2&q=) | [更新记录](http://code.google.com/p/midas2ansys/wiki/UpdateLog)
  * _Midas2ANSYS\_V1.0.0.18_ `[2009-08-31]`　　[下载](http://midas2ansys.googlecode.com/files/Midas2ANSYS_V1.0.0.18.rar)
  * _Midas2ANSYS\_V1.0.0.17_ `[2008-01-05]`　　[下载](http://midas2ansys.googlecode.com/files/Midas2ANSYS_V1.0.0.17.rar)
## 使用注意事项 ##
  1. 使用时建议用 MIDAS先把模型转化为国际单元制（KN-m）的`*.mgt`文件;
  1. MIDAS模型中最大节点号请不要超过99999；
  1. 需要安装[.Net Framework 2.0](http://www.microsoft.com/downloads/details.aspx?displaylang=zh-cn&FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5)或以上版本;