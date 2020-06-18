import { User, BusinessContactRecord } from '../interfaces'

/** Dummy user data. */
export const sampleUserData: User[] = [
  { id: 1, name: 'user-1' },
  { id: 2, name: 'user-2' },
  { id: 3, name: 'user-3' },
  { id: 4, name: 'user-4' },
]

export const sampleBusinessContactRecordData: BusinessContactRecord[] = [
  { "userId": 1, "id": 101153383, "name": "David Example", "email": "test1@example.com", "address": { "line1": "123 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153384, "name": "David Test", "email": "test2@example.com", "address": { "line1": "124 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153385, "name": "David Dev", "email": "test3@example.com", "address": { "line1": "125 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153386, "name": "David Production", "email": "test4@example.com", "address": { "line1": "126 main st", "line2": "c/o Good QA", "city": "Cloud City", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153387, "name": "David Staging", "email": "test5@example.com", "address": { "line1": "127 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153388, "name": "David Integration", "email": "test6@example.com", "address": { "line1": "128 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153389, "name": "David Documentation", "email": "test7@example.com", "address": { "line1": "129 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
  { "userId": 1, "id": 101153390, "name": "David Pipeline", "email": "test8@example.com", "address": { "line1": "130 main st", "line2": null, "city": "Newberg", "stateOrProvince": "OR", "postalCode": "97132" } },
]