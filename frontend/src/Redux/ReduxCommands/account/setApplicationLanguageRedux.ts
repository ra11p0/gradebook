import enUS from 'date-fns/locale/en-US';
import pl from 'date-fns/locale/pl';
import moment from 'moment';
import { registerLocale } from 'react-datepicker';
import i18n from '../../../i18n/config';
import { store } from '../../../store';
import ActionTypes from '../../ActionTypes/accountActionTypes';

const setLanguage = {
  type: ActionTypes.SetLanguage,
};
export default async (
  language: string,
  dispatch: any = store.dispatch
): Promise<void> => {
  switch (language) {
    case 'pl-PL':
    case 'pl':
      moment.locale('pl');
      registerLocale('pl', pl);
      await i18n.changeLanguage('pl');
      dispatch({ ...setLanguage, payload: { language: 'pl' } });
      break;
    case 'en-US':
    case 'en':
      moment.locale('en');
      registerLocale('en', enUS);
      await i18n.changeLanguage('en');
      dispatch({ ...setLanguage, payload: { language: 'en' } });
      break;
  }
};
