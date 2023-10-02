import { usePathname, useSearchParams } from "next/navigation";
import { useEffect } from "react";

export const useScrollTopOnRouteChange = () => {
  const pathname = usePathname();
  const searchParams = useSearchParams();

  useEffect(() => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  }, [pathname, searchParams]);
};
