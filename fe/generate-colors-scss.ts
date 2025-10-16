// file này để biên dịch màu sang biến (variables) trong scss
const path = './src/styles/_colors.scss';

import { colors } from './src/styles/colors';
import fs from 'fs';

const scss = Object.entries(colors)
  .map(([key, value]) => `$${key}: ${value};`)
  .join('\n');

fs.writeFileSync(path, scss);
console.log('Generated _colors.scss successfully!');
