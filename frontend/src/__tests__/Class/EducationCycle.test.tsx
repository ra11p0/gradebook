import React from 'react';
import { act, render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from '../../store';
import i18n from '../../i18n/config';
import { I18nextProvider } from 'react-i18next';
import EducationCycle from '../../Components/Class/EducationCycle';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';

describe('<EducationCycle />', () => {
  it('Should show no cycle attached', async () => {
    jest
      .spyOn(ClassesProxy.educationCycles, 'getEducationCyclesInClass')
      .mockResolvedValueOnce({
        data: {
          hasPreparedActiveEducationCycle: false,
        },
      } as any);
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <EducationCycle classGuid={'fakeClassGuid'} />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    expect(
      await screen.findByText('No education cycle attached to this class')
    ).toBeTruthy();
  });
  it('Should show cycle not configured', async () => {
    jest
      .spyOn(ClassesProxy.educationCycles, 'getEducationCyclesInClass')
      .mockResolvedValueOnce({
        data: {
          activeEducationCycle: {},
          activeEducationCycleGuid: 'fakeGuid',
          hasPreparedActiveEducationCycle: false,
        },
      } as any);
    await act(async () => {
      render(
        <Provider store={store}>
          <BrowserRouter>
            <I18nextProvider i18n={i18n}>
              <EducationCycle classGuid={'fakeClassGuid'} />
            </I18nextProvider>
          </BrowserRouter>
        </Provider>
      );
    });
    expect(
      await screen.findByText('Education cycle has not been configured')
    ).toBeTruthy();
    expect(
      await screen.findByRole('button', { name: 'Configure education cycle' })
    ).toBeTruthy();
  });
});
