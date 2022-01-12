import {Tag} from "./tag";

export interface Movie {
  id: string;
  name: string;
  isLiked: boolean;
  genres: string[];
  tags: Tag[];
}

