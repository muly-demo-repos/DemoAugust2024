import { MulyCreateNestedManyWithoutUsersInput } from "./MulyCreateNestedManyWithoutUsersInput";
import { MulyWhereUniqueInput } from "../muly/MulyWhereUniqueInput";
import { InputJsonValue } from "../../types";

export type UserCreateInput = {
  email?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  mulies?: MulyCreateNestedManyWithoutUsersInput;
  myMuly?: MulyWhereUniqueInput | null;
  password: string;
  roles: InputJsonValue;
  username: string;
};
