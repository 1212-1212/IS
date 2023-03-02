using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EShop.Domain.ViewModels
{
    public class CarPartsViewModel
    {

        public CarPartsViewModel() { }

        public CarPartsViewModel(List<CarPart>? carParts, string? searchString, Guid? carPartBrandId,  Guid? carPartTypeId,  Guid? carPartStageId)
        {
            CarParts = carParts;
            SearchString = searchString;
            CarPartBrandId = carPartBrandId;
            CarPartTypeId = carPartTypeId;
            CarPartStageId = carPartStageId;
        }

        public IEnumerable<CarPart>? CarParts { get; set; }
        public string? SearchString { get; set; }

        public Guid? CarPartBrandId { get; set; }

        public Guid? CarPartTypeId { get; set; }

        public Guid? CarPartStageId{ get; set; }

    }
}
