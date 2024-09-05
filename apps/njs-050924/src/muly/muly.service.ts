import { Injectable } from "@nestjs/common";

@Injectable()
export class MulyService {
  constructor() {}
  async MulyActionOne(args: string): Promise<string> {
    throw new Error("Not implemented");
  }
}
