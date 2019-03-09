using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class CompleteViewModel
    {
        // 完成的订单
        public List<CompleteOrderSQLModel> CompleteList{get;set;}
        // 总收入统计
        public double TotalRevenue{get;set;}

        // 当前时间
        public string NowData{get;set;} 

        // 账户统计
        public Dictionary<string,double> moneyMap {get;set;} 
    }
}