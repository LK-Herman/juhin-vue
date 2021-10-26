using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class FilterDeliveriesDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }

        public DateTime Date { get; set; }
        public Guid DeliveryId { get; set; }
        public int StatusId { get; set; }
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string PartNumber { get; set; }
        public string OrderingField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
