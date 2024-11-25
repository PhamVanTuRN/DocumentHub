export type PortfolioGet = {
  id: number;
  symbol: string;
  DocumentName: string;
  purchase: number;
  lastDiv: number;
  DocumentUrl: string;
  marketCap: number;
  comments: any;
};

export type PortfolioPost = {
  symbol: string;
};
