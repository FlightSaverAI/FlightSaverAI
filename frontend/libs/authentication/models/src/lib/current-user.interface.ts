export interface CurrentUser {
  id: number;
  username: string;
  email: string;
  userRole: string; // this property will need a specific type with roles (temporary solution);
}
