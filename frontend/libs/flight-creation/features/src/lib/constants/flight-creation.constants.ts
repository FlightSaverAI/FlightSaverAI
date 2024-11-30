export abstract class FlightCreationConstants {
  static readonly stepperConf = [
    {
      stepName: 'flight',
      stepNumber: 1,
    },
    {
      stepName: 'ticket',
      stepNumber: 2,
    },
    {
      stepName: 'rate-and-review',
      stepNumber: 3,
    },
  ];

  static readonly prevBtnConf = {
    imgSrc: 'global/assets/arrow.svg',
    imgClass: 'u-rotate-180',
  };

  static readonly nextBtnConf = {
    imgSrc: 'global/assets/arrow.svg',
  };
}
