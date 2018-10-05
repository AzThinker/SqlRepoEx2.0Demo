 # 2.0.2版本更新
 ### 2018.10.5
## 1、增加数据特性 
SqlRepoDbFieldAttribute
标识是否为数据字段，主要是因为
###（1）、部分类型拥有复杂属性；
###（2）、有些属性不是来源于数据库；
###（3）、用户在原来的代码中使用 SqlRepoEx ，减少字段与数据库字段之间的冲突；
## 2、增加属性判断器
 增加 SimpleWritablePropertyMatcher 属性判断器，
  1、增加SqlRepoDbFieldAttribute特性后，如果用户程序仍然为POJO类型，不必标识SqlRepoDbFieldAttribute时，用SimpleWritablePropertyMatcher
  2、如果明确要区分，就用WritablePropertyMatcher ；
 ~~~
  string ConnectionString = "Data Source=(Local);Initial Catalog=Northwind;User ID=test;Password=test";

            var connectionProvider = new ConnectionStringConnectionProvider(ConnectionString);

            MsSqlRepoFactory.UseConnectionProvider(connectionProvider);
            MsSqlRepoFactory.UseWritablePropertyMatcher(new SimpleWritablePropertyMatcher());

            var repository = MsSqlRepoFactory.Create<Customers>();
~~~
### 3、二进制数据支持
  增加对 byte[]类型的支持，但应注意，其 byte[]格式成 Convert.ToBase64String() 后，SQL字串的总体长度不能超过8000字符。

# SqlRepoEx2.0示例


![image](https://raw.githubusercontent.com/AzThinker/SqlRepoEx2.0Demo/master/Demos/GettingStartedStatic/SqlRepoEx1.1与2.0功能对比.png)
