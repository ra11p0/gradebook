import { act, render, screen } from '@testing-library/react';
import { I18nextProvider } from 'react-i18next';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { vi } from 'vitest';
import ClassesProxy from '../../ApiClient/Classes/ClassesProxy';
import EducationCycle from '../../Components/Class/EducationCycle/EducationCycle';
import i18n from '../../i18n/config';
import { store } from '../../store';

describe('<EducationCycle />', () => {
  it('Should show no cycle attached', async () => {
    vi.spyOn(
      ClassesProxy.educationCycles,
      'getEducationCyclesInClass'
    ).mockResolvedValueOnce({
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
    expect(await screen.findByText('educationCycleNotAttached')).toBeTruthy();
  });
  it('Should show cycle not configured', async () => {
    vi.spyOn(
      ClassesProxy.educationCycles,
      'getEducationCyclesInClass'
    ).mockResolvedValueOnce({
      data: {
        activeEducationCycle: { guid: 'fakeguid' },
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
    expect(await screen.findByText('educationCycleNotConfigured')).toBeTruthy();
    expect(
      await screen.findByRole('button', { name: 'configureEducationCycle' })
    ).toBeTruthy();
  });
  it('Should show cycle not started', async () => {
    vi.spyOn(
      ClassesProxy.educationCycles,
      'getEducationCyclesInClass'
    ).mockResolvedValueOnce({
      data: {
        activeEducationCycle: { guid: 'fakeguid' },
        activeEducationCycleGuid: 'fakeGuid',
        currentStepInstance: {},
        activeEducationCycleInstance: {},
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
      await screen.findByText('educationCycleNotStartedDescription')
    ).toBeTruthy();
  });
  it('Should show can start next cycle step', async () => {
    vi.spyOn(
      ClassesProxy.educationCycles,
      'getEducationCyclesInClass'
    ).mockResolvedValueOnce({
      data: {
        activeEducationCycle: { guid: 'fakeguid' },
        activeEducationCycleGuid: 'fakeGuid',
        currentStepInstance: {
          started: true,
        },
        nextStepInstance: {},
        activeEducationCycleInstance: {},
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
      await screen.findByText('educationCycleStartedDescription')
    ).toBeTruthy();
    expect(
      await screen.findByText('educationCycleStartedNotLastDescription')
    ).toBeTruthy();
  });
  it('Should show can finish cycle', async () => {
    vi.spyOn(
      ClassesProxy.educationCycles,
      'getEducationCyclesInClass'
    ).mockResolvedValueOnce({
      data: {
        activeEducationCycle: { guid: 'fakeguid' },
        activeEducationCycleGuid: 'fakeGuid',
        currentStepInstance: {
          started: true,
        },
        activeEducationCycleInstance: {},
        hasPreparedActiveEducationCycle: false,
      },
    } as any);
    await act(async () => {
      render(
        <Provider store={store}>
          <EducationCycle classGuid={'fakeClassGuid'} />
        </Provider>
      );
    });
    expect(
      await screen.findByText('educationCycleStartedDescription')
    ).toBeTruthy();
    expect(
      await screen.findByText('educationCycleStartedLastDescription')
    ).toBeTruthy();
  });
});
