export class CreatedAuthUser {
  externalId: string;
  userName: string;
  email: string;

  hotpQrCodeUri: string;
  totpQrCodeUri: string;

  secretInBase32: string;

  id: string;
}
