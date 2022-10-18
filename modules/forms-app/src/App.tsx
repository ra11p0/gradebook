import { useState } from "react";
import ReactSelect from "react-select";
import "./App.css";
import Forms from "./Components/Forms";
import ApplicationModes from "./Constraints/ApplicationModes";
import FieldTypes from "./Constraints/FieldTypes";

const modes = Object.values(ApplicationModes).map((value) => ({ value, label: value.toString() }));

function App() {
  const [mode, setMode] = useState(ApplicationModes.Edit);
  return (
    <>
      <div>
        <ReactSelect
          value={modes.find((t) => t.value == mode)}
          options={modes}
          onChange={(newValue) => setMode(newValue?.value ?? ApplicationModes.Edit)}
        />
      </div>
      <div>
        <Forms
          onDiscard={() => { console.log('discard') }}
          onSubmit={(vals) => { console.dir(vals) }}
          mode={mode}
          fields={[
            {
              uuid: '0.10882926347944588',
              name: 'Zadanie 1',
              type: FieldTypes.LongText,
              labels: [
                ''
              ],
              description: 'Teraz wykonaj 2+2 i zobaczymy jaka odpowiedz ci wyjdzie tam, no zobaczymy ',
              distinctValues: false,
              forbidPast: false,
              forbidFuture: false,
              isRequired: true
            },
            {
              uuid: '0.3249726315550314',
              name: 'Zadanie 2',
              type: FieldTypes.LongText,
              labels: [
                ''
              ],
              description: 'Wykonaj cos tam a potem cos tam zrob tak.',
              distinctValues: false,
              forbidPast: false,
              forbidFuture: false,
              isRequired: true
            },
            {
              uuid: '0.25029253413474617',
              name: 'Zadanie 3',
              type: FieldTypes.Checkbox,
              labels: [
                'pierwszy',
                'drugi',
                'trzeci'
              ],
              description: 'Jaki jest ciekawy film',
              distinctValues: true,
              forbidPast: false,
              forbidFuture: false,
              isRequired: true
            },
            {
              uuid: '0.9006005039636694',
              name: 'Jaka jest data wykonania testu',
              type: FieldTypes.Date,
              labels: [
                ''
              ],
              description: '',
              distinctValues: false,
              forbidPast: true,
              forbidFuture: true,
              isRequired: true
            },
            {
              uuid: '0.5791298314889974',
              name: 'Jaka data bedzie jutro',
              type: FieldTypes.Date,
              labels: [
                ''
              ],
              description: '',
              distinctValues: false,
              forbidPast: true,
              forbidFuture: false,
              isRequired: true
            }
          ]} />
      </div>

    </>
  );
}

export default App;
