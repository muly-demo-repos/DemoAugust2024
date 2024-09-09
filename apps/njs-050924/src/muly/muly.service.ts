import { Injectable } from "@nestjs/common";

@Injectable()
export class MulyService {
  constructor() {}
  async MulyActionOne(args: string): Promise<string> {
    throw new Error("Not implemented");
  }
  async MulyActionTwo(args: boolean): Promise<boolean> {
    throw new Error("Not implemented");
  }
}
