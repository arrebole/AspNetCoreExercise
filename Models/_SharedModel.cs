using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    /* 数据库中储存的菜单 */
    public class MenuItemSQLModel
    {

        // 菜编号
        public long Id { get; set; }
        // 分组
        public int Group { get; set; }
        // 菜名
        public string Name { get; set; }
        // 菜价格
        public double Price { get; set; }
        // 销量
        public int SalesVolume { get; set; }
    }

    // 数据库中储存的订单
    public class OrderSQLModel
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Detail { get; set; }
        public int Seat { get; set; }
        public double Money { get; set; }
    }

    public class CompleteOrderSQLModel
    {
        public long Id { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string Detail { get; set; }
        public int Seat { get; set; }
        public double Money { get; set; }
    }
    //post提交上的订单
    public class PostOrder
    {
        public int Seat { get; set; }
        public string Time { get; set; }

        public List<PostOrderMenuItem> MenuList;
    }

    // 提交的订单 list成员
    public class PostOrderMenuItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }

    // 用户登录 post信息
    public class LoginPostInfo
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class UpdateMenuInfo
    {
        public string UpdateMenuGroup { get; set; }
        public string UpdateMenuName { get; set; }
        public string UpdateMenuPrice { get; set; }
        public string UpdateMenuFunc { get; set; }


    }

    // 处理订单
    public class OrderProcess
    {
        public long Id { get; set; }
    }
}