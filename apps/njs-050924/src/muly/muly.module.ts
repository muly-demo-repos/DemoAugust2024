import { Module } from "@nestjs/common";
import { MulyService } from "./muly.service";
import { MulyController } from "./muly.controller";
import { MulyResolver } from "./muly.resolver";

@Module({
  controllers: [MulyController],
  providers: [MulyService, MulyResolver],
  exports: [MulyService],
})
export class MulyModule {}
