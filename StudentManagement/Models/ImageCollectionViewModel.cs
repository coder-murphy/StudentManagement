using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    /// <summary>
    /// 图像集展示模型
    /// </summary>
    public class ImageCollectionViewModel
    {
        /// <summary>
        /// 图像集
        /// </summary>
        public List<IFormFile> Images { get; set; }
    }
}
