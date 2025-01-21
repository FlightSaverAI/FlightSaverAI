import { inject } from '@angular/core';
import { StatisticsService } from '@flight-saver/statistics/data-access';
import { map } from 'rxjs';

export const createFlightOverviewChartConfig = () => {
  return inject(StatisticsService)
    .getFlightPassangerOverview()
    .pipe(
      map((data: any) => {
        const classData = Object.fromEntries(
          data.classDistribution.map(({ name, value }: any) => [name, value])
        );
        const seatData = Object.fromEntries(
          data.seatDistribution.map(({ name, value }: any) => [name, value])
        );
        const reasonData = Object.fromEntries(
          data.reasonDistribution.map(({ name, value }: any) => [name, value])
        );
        const continentsData = Object.fromEntries(
          data.continents.map(({ name, value }: any) => [name, value])
        );

        return [
          {
            title: {
              text: 'CLASS',
              left: 'center',
              textStyle: {
                color: '#FFFFFF',
              },
              top: '5%',
            },
            tooltip: {
              trigger: 'item',
            },
            legend: {
              orient: 'vertical',
              bottom: '5%',
              left: 'center',
              icon: 'circle',
              textStyle: {
                color: '#FFFFFF',
              },
              formatter: (name: string) => `${classData[name]}  ${name}`,
            },
            color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
            series: [
              {
                name: 'Access From',
                type: 'pie',
                radius: '50%',
                center: ['50%', '40%'],
                data: data.classDistribution,
                label: {
                  show: false,
                },
                emphasis: {
                  itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)',
                  },
                },
              },
            ],
          },
          {
            title: {
              text: 'SEAT',
              left: 'center',
              textStyle: {
                color: '#FFFFFF',
              },
              top: '5%',
            },
            tooltip: {
              trigger: 'item',
            },
            legend: {
              orient: 'vertical',
              bottom: '10%',
              left: 'center',
              icon: 'circle',
              textStyle: {
                color: '#FFFFFF',
              },
              formatter: (name: string) => `${seatData[name]}  ${name}`,
            },
            color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
            series: [
              {
                name: 'Access From',
                type: 'pie',
                radius: '50%',
                center: ['50%', '40%'],
                data: data.seatDistribution,
                label: {
                  show: false,
                },
                emphasis: {
                  itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)',
                  },
                },
              },
            ],
          },
          {
            title: {
              text: 'REASON',
              left: 'center',
              textStyle: {
                color: '#FFFFFF',
              },
              top: '5%',
            },
            tooltip: {
              trigger: 'item',
            },
            legend: {
              orient: 'vertical',
              bottom: '5%',
              left: 'center',
              icon: 'circle',
              textStyle: {
                color: '#FFFFFF',
              },
              formatter: (name: string) => `${reasonData[name]}  ${name}`,
            },
            color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF'],
            series: [
              {
                name: 'Access From',
                type: 'pie',
                radius: '50%',
                center: ['50%', '40%'],
                data: data.reasonDistribution,
                label: {
                  show: false,
                },
                emphasis: {
                  itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)',
                  },
                },
              },
            ],
          },
          {
            title: {
              text: 'CONTINENTS',
              left: 'center',
              textStyle: {
                color: '#FFFFFF',
              },
              top: '5%',
            },
            tooltip: {
              trigger: 'item',
            },
            legend: {
              orient: 'vertical',
              bottom: '5%',
              left: 'center',
              icon: 'circle',
              textStyle: {
                color: '#FFFFFF',
              },
              formatter: (name: string) => `${continentsData[name]}  ${name}`,
            },
            color: ['#B7C2E8', '#4B5169', '#FFFFFF', '#C4C4C4', '#7682AF', '#9747FF'],
            series: [
              {
                name: 'Access From',
                type: 'pie',
                radius: '50%',
                center: ['50%', '40%'],
                data: data.continents,
                label: {
                  show: false,
                },
                emphasis: {
                  itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)',
                  },
                },
              },
            ],
          },
        ];
      })
    );
};
