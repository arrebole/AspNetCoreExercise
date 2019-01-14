using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    /* 菜单基础数据 */
    public class MenuItemModel
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

    //客户端post上的订单
    public class Order
    {
        public Order()
        {
            menuList = new List<postMenuItem>();
        }
        public int seat { get; set; }
        public string time { get; set; }

        public List<postMenuItem> menuList;
    }

    public class postMenuItem
    {
        public int id { get; set; }
        public int count { get; set; }
    }
}