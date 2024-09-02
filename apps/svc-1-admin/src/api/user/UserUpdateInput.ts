import { MulyUpdateManyWithoutUsersInput } from "./MulyUpdateManyWithoutUsersInput";
import { MulyWhereUniqueInput } from "../muly/MulyWhereUniqueInput";
import { InputJsonValue } from "../../types";

export type UserUpdateInput = {
  email?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  mulies?: MulyUpdateManyWithoutUsersInput;
  myMuly?: MulyWhereUniqueInput | null;
  password?: string;
  roles?: InputJsonValue;
  username?: string;
};
