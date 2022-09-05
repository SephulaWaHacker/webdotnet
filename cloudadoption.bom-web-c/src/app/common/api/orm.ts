export interface Part {
  id?: number
  partNumber: string,
  quantity: number,
  unitType: 'SINGLE_PIECE' | 'SMALL_BOX' | 'LARGE_BOX',
  assembled: boolean,
}

export interface PartFamily {
  id?: number
  name: string,
  parts: Part[];
}

export interface Bom {
  billOfMaterialId: string,
  vehicleId: string,
  startDate?: string,
  endDate?: string,
  status: 'ACTIVE' | 'DRAFT' | 'DELETED',

  partFamilies: PartFamily[];
}
