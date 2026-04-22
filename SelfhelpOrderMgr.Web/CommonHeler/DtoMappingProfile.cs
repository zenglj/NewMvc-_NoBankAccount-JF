using Nelibur.ObjectMapper;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class DtoMappingProfile
    {
        /// <summary>
        /// 初始化映射规则
        /// </summary>
        public static void Initialize()
        {
            // 在这里添加你的映射规则
            // TinyMapper.Bind<源类型, 目标类型>((config) => 
            // {
            //     // 可以在这里进行属性重命名等复杂配置，简单的一致映射通常不需要配置
            // });

            // 示例 1：DTO 转 Model (UserDto -> User)
            // TinyMapper.Bind<UserDto, User>();

            // 示例 2：如果属性名不一致，可以手动映射
            // TinyMapper.Bind<OrderDto, OrderModel>(config =>
            // {
            //     config.Bind(src => src.OrderID, dest => dest.Id);
            //     config.Bind(src => src.CreateTime, dest => dest.CreateDate);
            // });

            TinyMapper.Bind<T_Stock_Search, StockQueryDto>();
            TinyMapper.Bind< StockQueryDto,T_Stock_Search> ();

            TinyMapper.Bind<T_Stock, StockAddDto>();
            TinyMapper.Bind<StockAddDto,T_Stock>();

            TinyMapper.Bind<T_StockDTL, StockDetailAddDto>();
            TinyMapper.Bind<StockDetailAddDto, T_StockDTL>();

            TinyMapper.Bind<List<T_StockDTL>, List<StockDetailAddDto>>();
            TinyMapper.Bind<List<StockDetailAddDto>, List<T_StockDTL>>();


            TinyMapper.Bind<T_Stock, StockImportExcelDto>();
            TinyMapper.Bind<StockImportExcelDto, T_Stock>();

            TinyMapper.Bind<List<T_Stock>, List<StockImportExcelDto>>();
            TinyMapper.Bind<List<StockImportExcelDto>, List<T_Stock>>();

            //商品库存数量查询dto映射
            TinyMapper.Bind<ViewGoodStockQty, StockQtyQueryDto>();
            TinyMapper.Bind<StockQtyQueryDto, ViewGoodStockQty>();
        }
    }
}


