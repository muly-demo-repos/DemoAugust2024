import { StringNullableFilter } from "../../util/StringNullableFilter";
import { StringFilter } from "../../util/StringFilter";
import { MulyListRelationFilter } from "../muly/MulyListRelationFilter";
import { MulyWhereUniqueInput } from "../muly/MulyWhereUniqueInput";

export type UserWhereInput = {
  email?: StringNullableFilter;
  firstName?: StringNullableFilter;
  id?: StringFilter;
  lastName?: StringNullableFilter;
  mulies?: MulyListRelationFilter;
  myMuly?: MulyWhereUniqueInput;
  username?: StringFilter;
};
