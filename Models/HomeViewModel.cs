using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class HomeViewModel
    {
        // 构造函数
        public HomeViewModel()
        {
            Soup = new List<MenuItemModel>();
            Dish = new List<MenuItemModel>();
            Rice = new List<MenuItemModel>();
        }
        
        // 菜单列表
        // 汤
        public List<MenuItemModel> Soup;

        // 主菜
        public List<MenuItemModel> Dish;

        // 米饭
        public List<MenuItemModel> Rice;

    }


}