import { NameValue } from '@shared/models';

export interface FlightPassengerOverviewStatistics {
  classDistribution: NameValue[];
  seatDistribution: NameValue[];
  reasonDistribution: NameValue[];
  continents: NameValue[];
}
