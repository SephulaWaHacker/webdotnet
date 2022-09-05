export class BomUrl {
  static readonly BOMS = '/api/v1/bom';
  static readonly BOM = '/api/v1/bom/{bomId}';
  static readonly VEHICLE_BOM = '/api/v1/bom/{bomId}?vehicleId={vehicleId}';
  static readonly BOM_PUBLISH = '/api/v1/bom/{bomId}/publish';
  static readonly PART_FAMILIES = '/api/v1/bom/{bomId}/part-family';
  static readonly PART_FAMILY = '/api/v1/bom/{bomId}/part-family/{partFamilyId}';
  static readonly PARTS = '/api/v1/bom/{bomId}/part-family/{partFamilyId}/part';
  static readonly PART = '/api/v1/bom/{bomId}/part-family/{partFamilyId}/part/{partId}';
}

export class PartFamilyUrl {
  static readonly PART_FAMILIES = '/api/v1/part-family';
  static readonly PART_FAMILY = '/api/v1/part-family/{partFamilyId}';
  static readonly PART_FAMILY_PARTS = '/api/v1/part-family/{partFamilyId}/part';
  static readonly PART_FAMILY_PART = '/api/v1/part-family/{partFamilyId}/part/{partId}';
}

export class PartUrl {
  static readonly PARTS = '/api/v1/part';
}
