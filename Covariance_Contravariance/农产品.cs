using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covariance_Contravariance
{
    class 农产品
    {
        public float 数量 { get; set; }
        public decimal 单价 { get; set; }
        public decimal 计算总价()
        {
            return 单价 * (decimal)数量;
        }
    }

    class 豆类 : 农产品
    {
    }

    class 绿豆 : 豆类
    {
    }
    class 黄豆 : 豆类
    {
    }

    class 西瓜 : 农产品
    {
    }

    interface I农产品生产者<out T> where T : 农产品   //T是协变的
    {
        public void 生产产品(float 数量, decimal 单价);
        public T 获取产品();

    }
    class 生产者类<T> : I农产品生产者<T> where T : 农产品, new()
    {
        private T? _产品;
        public void 生产产品(float 生产数量, decimal 生产单价)
        {
            _产品 = new T
            {
                数量 = 生产数量,
                单价 = 生产单价
            };
            Console.WriteLine($"完成商品生产：数量：{_产品.数量}，单价：{_产品.单价}。");

        }
        public T 获取产品()
        {
            T 产品打包;
            if (_产品 != null)
            {
                产品打包 = _产品;
                _产品 = null;
                return 产品打包;
            }
            else
            {  
                throw new InvalidOperationException("没有生产产品，工厂无库存");
            }            
        }
    }
    interface I农产品消费者<in T> where T : 农产品  //T是逆变的
    {
        public void 接收货物(T 货物);
    }

    class 消费者类<T> : I农产品消费者<T> where T : 农产品
    {
        public void 接收货物(T 货物)
        {
            Console.WriteLine($"消费者收到货物，种类：{货物.GetType().Name}，单价：{货物.单价}，数量：{货物.数量}。");
        }
    }
}
