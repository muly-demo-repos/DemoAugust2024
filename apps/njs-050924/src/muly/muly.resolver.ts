import * as graphql from "@nestjs/graphql";
import { MulyService } from "./muly.service";

export class MulyResolver {
  constructor(protected readonly service: MulyService) {}

  @graphql.Query(() => String)
  async MulyActionOne(
    @graphql.Args("args")
    args: string
  ): Promise<string> {
    return this.service.MulyActionOne(args);
  }

  @graphql.Query(() => Boolean)
  async MulyActionTwo(
    @graphql.Args("args")
    args: boolean
  ): Promise<boolean> {
    return this.service.MulyActionTwo(args);
  }
}
