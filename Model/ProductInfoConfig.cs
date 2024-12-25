namespace WorkToolsSln.Model
{
    class ProductInfoConfig
    {

        public bool IsHuman { get; set; }

        public bool IsPets { get; set; }

        public bool IsWorkStation { get; set; }

        public bool IsMovable { get; set; }

        public bool IsCT { get; set; }

        public string Index { get; set; } = string.Empty;
        public string RowNumber { get; set; } = string.Empty;
        public string SliceNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string TubeName { get; set; } = string.Empty;
        public string HVName { get; set; } = string.Empty;
        public string SlipRingName { get; set; } = string.Empty;
        public string CollimationName { get; set; } = string.Empty;
        public string DetectorName { get; set; } = string.Empty;
        public string GantryName { get; set; } = string.Empty;
        public string GDisplayName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string OCName { get; set; } = string.Empty;
        public string GPUName { get; set; } = string.Empty;
        public string PDUName { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string ManufacturerEn { get; set; } = string.Empty;
    }
}
