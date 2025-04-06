export enum Genre {
  Fiction = 0,
  NonFiction = 1,
  Mystery = 2,
  ScienceFiction = 3,
  Fantasy = 4,
  Romance = 5,
  Thriller = 6,
  Horror = 7,
  Biography = 8,
  History = 9,
  Poetry = 10,
  Drama = 11,
  Comedy = 12,
  Adventure = 13,
  Children = 14,
  Educational = 15,
  Other = 16
}

export function getGenreName(genre: Genre): string {
  return Genre[genre];
} 