export interface IShortUrlModel {
  key: string;
  url: string;
  createdUtc: Date;
  expiresUtc: Date;
  creatorId: string;
}
