import { Muly } from "../muly/Muly";
import { JsonValue } from "type-fest";

export type User = {
  createdAt: Date;
  email: string | null;
  firstName: string | null;
  id: string;
  lastName: string | null;
  mulies?: Array<Muly>;
  myMuly?: Muly | null;
  roles: JsonValue;
  updatedAt: Date;
  username: string;
};
