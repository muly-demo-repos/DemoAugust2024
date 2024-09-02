import { User } from "../user/User";

export type Muly = {
  createdAt: Date;
  id: string;
  myUser?: User | null;
  updatedAt: Date;
  user?: User | null;
};
