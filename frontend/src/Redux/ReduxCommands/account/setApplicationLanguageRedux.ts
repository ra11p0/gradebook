import pl from 'date-fns/locale/pl';
import enUS from 'date-fns/locale/en-US';
import moment from 'moment';
import { registerLocale } from 'react-datepicker';
import ActionTypes from '../../ActionTypes/accountActionTypes';

const setLanguage = {
  type: ActionTypes.SetLanguage,
};
export default (dispatch: any, language: string): void => {
  dispatch({ ...setLanguage, payload: { language } });
  switch (language) {
    case 'pl':
      moment.locale('pl');
      registerLocale('pl', pl);
      break;
    case 'en':
      moment.locale('en');
      registerLocale('en', enUS);
      break;
  }
};
