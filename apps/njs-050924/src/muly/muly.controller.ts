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
    body: string
  ): Promise<string> {
        return this.service.MulyActionOne(body);
      }
}
