using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    /* 菜单基础数据用于储存在数据库 */
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

    public class OrderSQLModel
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Detail { get; set; }
        public int Seat { get; set; }
        public double Money { get; set; }
    }

    //客户端post上的订单
    public class PostOrder
    {
        public int seat { get; set; }
        public string time { get; set; }

        public List<PostOrderMenuItem> menuList;
    }

    public class PostOrderMenuItem
    {
        public int id { get; set; }
        public int count { get; set; }
    }

    // 用户登录 post信息
    public class LoginPostInfo
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

}