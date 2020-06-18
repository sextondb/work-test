// You can include shared interfaces/types in a separate file
// and then use them in any component by importing them. For
// example, to import the interface below do:
//
// import User from 'path/to/interfaces';

export type User = {
  id: number
  name: string
}

export type BusinessContactRecord = {
  userId: number,
  id?: number,
  name?: string | null,
  email?: string | null,
  address?: BusinessContactAddress | null
}

export type BusinessContactAddress = {
  line1?: string | null,
  line2?: string | null,
  city?: string | null,
  stateOrProvince?: string | null,
  postalCode?: string | null
}