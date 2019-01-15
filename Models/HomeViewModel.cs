using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class HomeViewModel
    {
        // 构造函数
        public HomeViewModel()
        {
            Soup = new List<MenuItemSQLModel>();
            Dish = new List<MenuItemSQLModel>();
            Rice = new List<MenuItemSQLModel>();
        }
        
        // 菜单列表
        // 汤
        public List<MenuItemSQLModel> Soup;

        // 主菜
        public List<MenuItemSQLModel> Dish;

        // 米饭
        public List<MenuItemSQLModel> Rice;

    }

}