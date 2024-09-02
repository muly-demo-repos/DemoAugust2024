import { StringFilter } from "../../util/StringFilter";
import { UserWhereUniqueInput } from "../user/UserWhereUniqueInput";

export type MulyWhereInput = {
  id?: StringFilter;
  myUser?: UserWhereUniqueInput;
  user?: UserWhereUniqueInput;
};
