using System.Collections.Concurrent;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Responses;

namespace BMW.CloudAdoption.BOM.Domain;

public class BomQueue : ConcurrentQueue<BomResponse> { }