export interface FlightPassengerOverviewStatistics {
  classDistribution: NameValue[];
  seatDistribution: NameValue[];
  reasonDistribution: NameValue[];
  continents: NameValue[];
}

interface NameValue {
  name: string;
  value: number;
}
