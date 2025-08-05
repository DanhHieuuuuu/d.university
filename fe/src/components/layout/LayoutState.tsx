'use client';

import { PropsWithChildren } from 'react';

function LayoutState(props: PropsWithChildren) {
  const { children } = props;
  // Call initial api in here like listPost, list, ...

  return <div>{children}</div>;
}

export default LayoutState;