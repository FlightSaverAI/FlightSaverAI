import { inject } from '@angular/core';
import { StatisticsService } from '@flight-saver/statistics/data-access';
import { map, Observable } from 'rxjs';
import { EChartsOption } from 'echarts';
import { NameValue } from '@shared/models';

export const createFlightOverviewChartConfig = (): Observable<EChartsOption[]> => {
  return inject(StatisticsService)
    .getFlightPassangerOverview()
    .pipe(
      map((data) => {
        const classData = createDataObject(data.classDistribution);
        const seatData = createDataObject(data.seatDistribution);
        const reasonData = createDataObject(data.reasonDistribution);
        const continentsData = createDataObject(data.continents);

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

const createDataObject = (dataArray: NameValue[]) =>
  Object.fromEntries(dataArray.map(({ name, value }) => [name, value]));
