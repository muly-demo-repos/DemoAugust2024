import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import * as errors from "../errors";
import { MulyService } from "./muly.service";

@swagger.ApiTags("mulies")
@common.Controller("mulies")
export class MulyController {
  constructor(protected readonly service: MulyService) {}

  @common.Get("/:id/muly-action-one")
  @swagger.ApiOkResponse({
    type: String
  })
  @swagger.ApiNotFoundResponse({
    type: errors.NotFoundException
  })
  @swagger.ApiForbiddenResponse({
    type: errors.ForbiddenException
  })
  async MulyActionOne(
    @common.Body()
    body: boolean
  ): Promise<string> {
        return this.service.MulyActionOne(body);
      }

  @common.Get("/:id/muly-action-two")
  @swagger.ApiOkResponse({
    type: Boolean
  })
  @swagger.ApiNotFoundResponse({
    type: errors.NotFoundException
  })
  @swagger.ApiForbiddenResponse({
    type: errors.ForbiddenException
  })
  async MulyActionTwo(
    @common.Body()
    body: boolean
  ): Promise<boolean> {
        return this.service.MulyActionTwo(body);
      }
}
