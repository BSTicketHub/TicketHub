using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECPayLibrary.Model
{
    class AioBaseModel
    {
        /// <summary>
        /// 特店交易編號
        /// </summary>
        public string MerchantTradeNo { get; set; }

        /// <summary>
        /// 特店交易時間
        /// </summary>
        public string MerchantTradeDate { get; set; }

        /// <summary>
        /// 交易金額
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// 交易描述
        /// </summary>
        public string TradeDesc { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 選擇預設付款方式
        /// </summary>
        public string ChoosePayment { get; set; }

        /// <summary>
        /// 檢查碼
        /// </summary>
        public string CheckMacValue { get; set; }
    }
}
